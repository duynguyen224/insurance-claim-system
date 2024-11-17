jQuery(function ($) {
  const $sectionLogin = $("#sectionLogin");
  const $sectionSendClaim = $("#sectionSendClaim");
  const $sectionClaimManagement = $("#sectionClaimManagement");
  const $sectionIntro = $("#sectionIntro");

  initUI();

  // Btn Login
  $("#btnLogin").click(function () {
    $sectionSendClaim.hide();
    $sectionIntro.hide();
    $sectionLogin.show();
  });

  $("#btnCloseLogin").click(function () {
    $sectionLogin.hide();
    $sectionIntro.show();
  });

  // Btn Logout
  $("#btnLogout").click(function () {
    clearLocalStorage();
    reloadWindow();
  });

  // Btn Send claim
  $("#btnSendClaim").click(function () {
    $sectionLogin.hide();
    $sectionIntro.hide();
    $sectionSendClaim.show();
  });

  $("#btnCloseSendClaim").click(function () {
    $sectionSendClaim.hide();

    // If user is logged in, do nothing
    const userInfo = getUserInfo();
    if (userInfo != null) {
      return;
    }

    // Anonymous user
    $sectionIntro.show();
  });

  // Btn Claim management
  $("#btnClaimManagement").click(function () {
    $sectionClaimManagement.show();
  });

  $("#btnCloseClaimManagement").click(function () {
    $sectionClaimManagement.hide();
  });

  // Select user
  $("#selectFilterUser").change(function () {
    const userId = $(this).val();
    const status = $("#selectFilterStatus").val();
    const submitDate = $("#iptFilterDate").val();

    fetchClaims(userId, status, submitDate);
  });

  // Select status
  $("#selectFilterStatus").change(function () {
    const userId = "";
    const status = $(this).val();
    const submitDate = $("#iptFilterDate").val();

    fetchClaims(userId, status, submitDate);
  });

  // Change submit date
  $("#iptFilterDate").change(function () {
    const userId = "";
    const status = $("#selectFilterStatus").val();
    const submitDate = $(this).val();

    fetchClaims(userId, status, submitDate);
  });

  // Btn Process claim
  $(document).on("click", ".btnProcessClaim", function () {
    if (
      confirm(
        "The claim will be randomly approved or rejected based on a 50/50 probability.\nDo you want to continue?"
      )
    ) {
      const claimId = $(this).closest("tr").attr("data-claim-id");
      processClaim(claimId);
    }
  });

  // Btn Delete claim
  $(document).on("click", ".btnDeleteClaim", function () {
    if (confirm("Are you sure to delete this claim?")) {
      const claimId = $(this).closest("tr").attr("data-claim-id");
      deleteClaim(claimId);
    }
  });

  // Btn Edit claim
  $(document).on("click", ".btnEditClaim", function () {
    // Open modal
    $("#modalUpdateClaim").modal("show");

    const claimId = $(this).closest("tr").attr("data-claim-id");
    const customerName = $(this).closest("tr").attr("data-customer-name");
    const amount = $(this).closest("tr").attr("data-amount");
    const description = $(this).closest("tr").attr("data-description");

    $("#iptClaimId").val(claimId);

    // Bind data to modal
    const formData = [
      { name: "HiddenClaimId", value: claimId },
      { name: "CustomerName", value: customerName },
      { name: "Amount", value: amount },
      { name: "Description", value: description },
    ];

    autofillForm("#formUpdateClaim", formData);
  });

  // Btn login here
  $("#btnLoginHere").click(function () {
    $sectionLogin.show();
    $sectionIntro.hide();
  });

  // Btn file claim here
  $("#btnFileClaimHere").click(function () {
    $sectionSendClaim.show();
    $sectionIntro.hide();
  });
});

function reloadWindow() {
  window.location.reload();
}

function getUserInfo() {
  const userInfo = localStorage.getItem("userInfo");
  return JSON.parse(userInfo);
}

function clearLocalStorage() {
  localStorage.removeItem("userInfo");
  localStorage.removeItem("token");
}

function initUI() {
  const $sectionLogin = $("#sectionLogin");
  const $sectionSendClaim = $("#sectionSendClaim");
  const $sectionClaimManagement = $("#sectionClaimManagement");
  const $sectionIntro = $("#sectionIntro");

  const userInfo = getUserInfo();

  if (userInfo == null) {
    $sectionIntro.show();
    $sectionSendClaim.hide();
    return;
  }

  // Hide element
  $("#btnLogin").hide();

  if (userInfo.Roles.includes(ROLE_USER)) {
    $("#selectFilterUserContainer").hide();
  }

  if (userInfo.Roles.includes(ROLE_ADMIN)) {
    $("#btnSendClaim").hide();
    $("#sectionSendClaim").hide();
    $("#sectionClaimManagement").show();
  }

  // Show element
  $("#btnLogout").show();
  $("#btnClaimManagement").show();
  $("#txtUserName").html(userInfo.FullName);
  $(`#formSendClaim input[name='CustomerName']`).prop("readonly", true);
  $(`#formSendClaim input[name='CustomerName']`).val(userInfo.FullName);

  // Get data for Claims management
  fetchClaims();

  // Fetch data select filter users
  fetchUsers();
}

function fetchClaims(userId = "", status = "", submitDate = "") {
  // If anonymous user, stop here
  let token = localStorage.getItem("token");
  token = JSON.parse(token);
  if (isNullOrEmpty(token)) {
    return;
  }

  const request = {
    UserId: userId,
    Status: status,
    SubmitDate: submitDate,
  };

  // Convert the request object into query string parameters
  const queryString = $.param(request);

  $.ajax({
    type: HTTP_METHOD.GET,
    url: `${BASE_URL + CLAIM_API}?${queryString}`,
    success: function (response) {
      const claims = response.Data;

      const htmlNoData = `<tr>
                            <td colspan="100" class="text-danger">No data</td>
                          </tr>`;

      if (claims.length == 0) {
        $("#tblClaimsManagement tbody").html(htmlNoData);
        return;
      }

      let html = "";

      claims.forEach((item, index) => {
        html += `<tr data-claim-id="${item.Id}" 
                      data-customer-name="${item.CustomerName}" 
                      data-amount="${item.Amount}" 
                      data-description="${item.Description}">
                    <th>${++index}</th>
                    <td>${item.CustomerName}</td>
                    <td class="text-end">${item.Amount}</td>
                    <td>
                      <textarea readonly rows="3" cols="40" class="text-start form-control">${
                        item.Description
                      }</textarea>
                    </td>
                    <td>
                      ${formatToDate(item.SubmitDate)}
                    </td>
                    <td>
                      ${renderStatusBadge(item.Status)}
                    </td>
                    <td>
                      ${renderEditLink(item.Status)}
                      ${renderDeleteLink(item.Status)}
                      ${renderProcessLink(item.Status)}
                    </td>
                  </tr>`;
      });

      // Draw claims management table body
      $("#tblClaimsManagement tbody").html(html);
    },
    error: function (error) {
      const errorResponse = error.responseJSON;
      showAlert(
        "#xxxxxxxxxxxxxxx",
        errorResponse?.Message || "An error occurred",
        "danger"
      );
    },
  });
}

function fetchUsers() {
  // If anonymous user, stop here
  let token = localStorage.getItem("token");
  token = JSON.parse(token);
  if (isNullOrEmpty(token)) {
    return;
  }

  $.ajax({
    type: HTTP_METHOD.GET,
    url: `${BASE_URL + USER_API}`,
    success: function (response) {
      const users = response.Data;

      let options = `<option selected value="">All</option>`;
      users.forEach(function (item) {
        options += `<option value="${item.Id}">${item.FullName}</option>`;
      });

      $("#selectFilterUser").html(options);
    },
    error: function (error) {
      const errorResponse = error.responseJSON;
      showAlert(
        "#xxxxxxxxxxxxxxx",
        errorResponse?.Message || "An error occurred",
        "danger"
      );
    },
  });
}

function renderStatusBadge(status) {
  switch (status) {
    case CLAIM_STATUS.PENDING:
      return `<span class="badge rounded-pill text-bg-info">Pending</span>`;
      break;
    case CLAIM_STATUS.APPROVED:
      return `<span class="badge rounded-pill text-bg-success">Approved</span>`;
      break;
    case CLAIM_STATUS.REJECTED:
      return `<span class="badge rounded-pill text-bg-danger">Rejected</span>`;
      break;
  }
}

function renderDeleteLink(status) {
  const roles = getUserInfo().Roles;

  if (status == CLAIM_STATUS.PENDING && roles.includes(ROLE_USER)) {
    return `<a class="hover-pointer btnDeleteClaim">Delete</a><br>`;
  }

  return "";
}

function renderProcessLink(status) {
  const roles = getUserInfo().Roles;

  if (status == CLAIM_STATUS.PENDING && roles.includes(ROLE_ADMIN)) {
    return `<a class="hover-pointer btnProcessClaim">Process</a><br>`;
  }

  return "";
}

function renderEditLink(status) {
  const roles = getUserInfo().Roles;

  if (status == CLAIM_STATUS.PENDING && roles.includes(ROLE_USER)) {
    return `<a class="hover-pointer btnEditClaim">Edit</a><br>`;
  }

  return "";
}

function processClaim(claimId) {
  $.ajax({
    type: HTTP_METHOD.PUT,
    url: BASE_URL + CLAIM_API + `/${claimId}/process`,
    success: function (response) {
      // Show alert
      showAlert(
        "#tblClaimsManagementContainer",
        response.Message,
        "success",
        "prepend"
      );

      // Re-fetch claims
      fetchClaims();
    },
    error: function (error) {
      const errorResponse = error.responseJSON;
      showAlert("#xxxxxxxxx", errorResponse.Message, "danger");
    },
  });
}

function deleteClaim(claimId) {
  $.ajax({
    type: HTTP_METHOD.DELETE,
    url: BASE_URL + CLAIM_API + `/${claimId}`,
    success: function (response) {
      // Show alert
      showAlert(
        "#tblClaimsManagementContainer",
        response.Message,
        "success",
        "prepend"
      );

      // Re-fetch claims
      fetchClaims();
    },
    error: function (error) {
      const errorResponse = error.responseJSON;
      showAlert("#xxxxxxxxx", errorResponse.Message, "danger");
    },
  });
}
