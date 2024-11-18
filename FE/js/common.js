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
// == HTTP METHODS ==
// ==================
const HTTP_METHOD = {
  GET: "GET",
  POST: "POST",
  PUT: "PUT",
  DELETE: "DELETE",
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
  if (jqxhr.status === 401) {
    alert(`Token expired, please login again to continue.`);
    localStorage.removeItem("userInfo");
    localStorage.removeItem("token");
    reloadWindow();
  } else if (jqxhr.status === 403) {
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
  $(".alert").slideUp(4000);
}

function autofillForm(formId, data) {
  data.forEach(({ name, value }) => {
    const field = $(`${formId} [name="${name}"]`);
    if (field.length) {
      field.val(value);
    }
  });
}

function showServerValidationErrors(formId, errors) {
  // Clear previous error messages
  $(".error-message").remove();

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
