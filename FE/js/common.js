// ================
// == API ROUTES ==
// ================
const BASE_URL = "https://localhost:7139/";
const AUTH_API = "api/auth/login";
const CLAIM_API = "api/insurance-claims";

// =================
// == Http method ==
// =================
const HTTP_METHOD = {
  GET: "GET",
  POST: "POST",
  PUT: "PUT",
  DELETE: "DELETE",
};

// ================
// == Ajax setup ==
// ================
$.ajaxSetup({
  contentType: "application/json",
  beforeSend: function (xhr) {
    const token = localStorage.getItem("authToken");
    if (token) {
      xhr.setRequestHeader("Authorization", "Bearer " + token);
    }
  },
});

// ======================
// == Common functions ==
// ======================
function showAlert(formId, message, type) {
  const alertHtml = `<div class="alert alert-${type} mt-3 text-center" role="alert">
                      ${message}
                    </div>`;

  $(formId).append(alertHtml);

  // Hide
  $('.alert').slideUp(4000);
}
