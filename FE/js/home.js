jQuery(function ($) {
  const $sectionLogin = $("#sectionLogin");
  const $sectionSendClaim = $("#sectionSendClaim");
  const $sectionClaimManagement = $("#sectionClaimManagement");

  initUI();

  // Btn Login
  $("#btnLogin").click(function () {
    $sectionLogin.show();
  });

  $("#btnCloseLogin").click(function () {
    $sectionLogin.hide();
  });

  // Btn Logout
  $("#btnLogout").click(function () {
    clearLocalStorage();
    reloadWindow();
  });

  // Btn Send claim
  $("#btnSendClaim").click(function () {
    $sectionSendClaim.show();
  });

  $("#btnCloseSendClaim").click(function () {
    $sectionSendClaim.hide();
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
    alert("Get users not implemented");
    $("#selectFilterUser").val("'");
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
    if (confirm("The claim will be randomly approved or rejected based on a 50/50 probability. \n Do you want to continue?")) {
      const claimId = $(this).closest('tr').attr('data-claim-id');
      processClaim(claimId);
    }
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
  const userInfo = getUserInfo();

  if (userInfo == null) {
    return;
  }

  // Hide element
  $("#btnLogin").hide();

  // Show element
  $("#btnLogout").show();
  $("#btnClaimManagement").show();
  $("#txtUserName").html(userInfo.FullName);
  $(`#formSendClaim input[name='CustomerName']`).prop("readonly", true);
  $(`#formSendClaim input[name='CustomerName']`).val(userInfo.FullName);

  // Get data for Claims management
  fetchClaims();
}

function fetchClaims(userId = "", status = "", submitDate = "") {
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
        html += `<tr data-claim-id="${item.Id}">
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
                      <a class="hover-pointer btnDetailClaim">Detail</a>
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

function renderProcessLink(status) {
  const roles = getUserInfo().Roles;

  if (status == CLAIM_STATUS.PENDING && roles.includes(ROLE_ADMIN)) {
    return `<a class="hover-pointer btnProcessClaim">Process</a>`;
  } else {
    return "";
  }
}

function processClaim(claimId) {
  $.ajax({
    type: HTTP_METHOD.PUT,
    url: BASE_URL + CLAIM_API + `/${claimId}/process`,
    success: function (response) {
      // Re-fetch claims
      fetchClaims();
    },
    error: function (error) {
      const errorResponse = error.responseJSON;
      showAlert("#xxxxxxxxx", errorResponse.Message, "danger");
    },
  });
}
