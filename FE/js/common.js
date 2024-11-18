// ================
// == API ROUTES ==
// ================
const BASE_URL = "http://localhost:5071/"; // * Edit with your port
const AUTH_API = "api/auth/login";
const CLAIM_API = "api/insurance-claims";
const USER_API = "api/users";

// ================
// == ROLES =======
// ================
const ROLE_ADMIN = "admin";
const ROLE_USER = "user";

// ==================
// == CLAIM STATUS ==
// ==================
const CLAIM_STATUS = {
  PENDING: 0,
  APPROVED: 1,
  REJECTED: 2,
};

// ==================
// == HTTP ==========
// ==================
const HTTP_METHOD = {
  GET: "GET",
  POST: "POST",
  PUT: "PUT",
  DELETE: "DELETE",
};

const HTTP_STATUS_CODE = {
  BAD_REQUEST_400: 400,
  UNAUTHORIZED_401: 401,
  FORBIDDEN_403: 403,
  INTERNAL_SERVER_ERROR_500: 500,
};

// ================
// == AJAX SETUP ==
// ================
// Set default ajax header
$.ajaxSetup({
  contentType: "application/json",
  beforeSend: function (xhr) {
    let token = localStorage.getItem("token");
    token = JSON.parse(token);

    if (token) {
      xhr.setRequestHeader("Authorization", "Bearer " + token);
    }
  },
});

// Handle global ajax error
$(document).ajaxError(function (event, jqxhr, settings, exception) {
  if (jqxhr.status == HTTP_STATUS_CODE.BAD_REQUEST_400) {
    return;
  } else if (jqxhr.status == HTTP_STATUS_CODE.UNAUTHORIZED_401) {
    const invalidCredentialMessage = "Invalid credentials";
    const jqxhrMessage = jqxhr.responseJSON.Message;

    if (
      !isNullOrEmpty(jqxhrMessage) &&
      jqxhrMessage == invalidCredentialMessage
    ) {
      return;
    }

    alert(`Token expired, please login again to continue.`);
    localStorage.removeItem("userInfo");
    localStorage.removeItem("token");
    reloadWindow();
  } else if (jqxhr.status == HTTP_STATUS_CODE.FORBIDDEN_403) {
    alert(`Sorry, you don't have permission to access this resource.`);
    localStorage.removeItem("userInfo");
    localStorage.removeItem("token");
    reloadWindow();
  } else {
    console.error(`Error: ${jqxhr.status} - ${jqxhr.statusText}`);
    alert("An unexpected error occurred. Please try again.");
  }
});

// ======================
// == COMMON FUNCTIONS ==
// ======================
function isNullOrEmpty(str) {
  return !str || str.trim().length === 0;
}

function showAlert(containerId, message, type, position = "append") {
  const alertHtml = `<div class="alert alert-${type} mt-3 text-center" role="alert">
                      ${message}
                    </div>`;

  if (position == "append") {
    $(containerId).append(alertHtml);
  } else {
    $(containerId).prepend(alertHtml);
  }

  // Hide
  if (type == "success") {
    $(".alert").slideUp(3000);
  } else if (type == "danger") {
    $(".alert").slideUp(4000);
  }
}

function autofillForm(formId, data) {
  data.forEach(({ name, value }) => {
    const field = $(`${formId} [name="${name}"]`);
    if (field.length) {
      field.val(value);
    }
  });
}

function showServerValidationErrors(formId, error) {
  // If status code is not 400, do nothing
  if (error.status != 400) {
    return;
  }

  // Clear previous error messages
  $(".error-message").remove();
  $(".error").remove();

  const errors = error.responseJSON.Errors;
  console.log(errors);
  // Loop through errors and append messages below input fields
  Object.entries(errors).forEach(([fieldName, messages]) => {
    const field = $(`${formId} [name="${fieldName}"]`);
    if (field.length) {
      const errorMessage = messages.join(", ");
      field.after(`<label class="error">${errorMessage}</label>`);
    }
  });
}

function formatToDate(dateString) {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0"); // Month is zero-based
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`;
}
