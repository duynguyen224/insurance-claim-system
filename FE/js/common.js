// ================
// == API ROUTES ==
// ================
const BASE_URL = "https://localhost:7139/";
const AUTH_API = "api/auth/login";
const CLAIM_API = "api/insurance-claims";

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

// ======================
// == COMMON FUNCTIONS ==
// ======================
function isNullOrEmpty(str) {
  return !str || str.trim().length === 0;
}

function showAlert(formId, message, type) {
  const alertHtml = `<div class="alert alert-${type} mt-3 text-center" role="alert">
                      ${message}
                    </div>`;

  $(formId).append(alertHtml);

  // Hide
  $(".alert").slideUp(4000);
}

function autofillForm(formId, data) {
  data.forEach(({ name, value }) => {
    const field = $(`${form} [name="${name}"]`);
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
  const month = String(date.getMonth() + 1).padStart(2, '0'); // Month is zero-based
  const day = String(date.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
}