jQuery(function ($) {
  $("#formLogin").validate({
    rules: {
      email: {
        required: true,
        email: true,
      },
      password: {
        required: true,
        minlength: 6,
      },
    },
    messages: {
      email: {
        required: "Please enter your email address",
        email: "Please enter a valid email address",
      },
      password: {
        required: "Please enter your password",
        minlength: "Your password must be at least 6 characters long",
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
          console.log(response);
          showAlert("#formLogin", response.Message, "success");
        },
        error: function (error) {
          console.log(error.responseJSON.Message)
          showAlert("#formLogin", error.responseJSON.Message, "danger");
        },
      });
    },
  });
});
