jQuery(function ($) {
  $("#formLogin").validate({
    rules: {
      Email: {
        required: true,
        email: true,
      },
      Password: {
        required: true,
      },
    },
    submitHandler: function (form) {
      const formData = {
        email: $("#email").val(),
        password: $("#password").val(),
      };

      $.ajax({
        type: HTTP_METHOD.POST,
        url: BASE_URL + AUTH_API,
        data: JSON.stringify(formData),
        success: function (response) {
          // Show success alert
          showAlert("#formLogin", response.Message, "success");

          // Save data to local storage
          const data = response.Data;
          const userInfo = data.User;
          const token = data.Token;
          localStorage.setItem("userInfo", JSON.stringify(userInfo));
          localStorage.setItem("token", JSON.stringify(token));

          // Reload window
          setTimeout(() => {
            reloadWindow()
          }, 4000);
        },
        error: function (error) {
          const errorResponse = error.responseJSON;
          showServerValidationErrors('#formLogin', error);
          showAlert("#formLogin", errorResponse.Message, "danger");
        },
      });
    },
  });
});
