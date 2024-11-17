jQuery(function ($) {
  $("#formUpdateClaim").validate({
    rules: {
      CustomerName: {
        required: true,
        maxlength: 50,
      },
      Amount: {
        required: true,
        digits: true,
        range: [0.0, 2147483647],
      },
      Description: {
        required: true,
        maxlength: 255,
      },
    },
    submitHandler: function (form) {
      const formData = {
        customerName: $("#iptCustomerName").val(),
        amount: $("#iptAmount").val(),
        description: $("#iptDescription").val(),
      };

      const claimId = $("#iptClaimId").val();

      $.ajax({
        type: HTTP_METHOD.PUT,
        url: BASE_URL + CLAIM_API + `/${claimId}`,
        data: JSON.stringify(formData),
        success: function (response) {
          // Reset form
          const formData = [
            { name: "Amount", value: "" },
            { name: "Description", value: "" },
          ];

          autofillForm("#formUpdateClaim", formData);

          // Close modal
          $("#modalUpdateClaim").modal("hide");

          // Show success alert
          showAlert("#tblClaimsManagementContainer", response.Message, "success", 'prepend');

          // Re-fetch claims
          fetchClaims();
        },
        error: function (error) {
          const errorResponse = error.responseJSON;
          showServerValidationErrors("#formUpdateClaim", errorResponse.Errors);
          showAlert("#formUpdateClaim", errorResponse.Message, "danger");
        },
      });
    },
  });
});
