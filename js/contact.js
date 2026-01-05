let usersinfo = [];
// اضافه کردن اطلاعات برای تماس با ما
document.getElementById("contactForm").addEventListener("submit", function (e) {
  debugger  
 console.log("mobina")

  let name = document.getElementById("name").value;
  let email = document.getElementById("email").value;
  let phoneNumber = document.getElementById("phoneNumber").value;
  let massage = document.getElementById("message").value;
  console.log(name)
  console.log(email)
  console.log(phoneNumber)
  console.log(massage)

  let userinfo = {
    name: name,
    email: email,
    phoneNumber: phoneNumber,
    massage: massage,
  };

  usersinfo.push(userinfo);
  console.log(usersinfo)
  fetch("http://localhost:5013/api/ContactUs", {
    method: "POST",
    headers: {
      "Content-Type": "application/json", // فرستادن JSON
    },
    body: JSON.stringify(userinfo),
  })
    .then((response) => response.text()) // یا response.json() اگر JSON برگرده
    .then((data) => {
      console.log("پاسخ سرور:", data);
    })
    .catch((error) => console.error("خطا در ارسال:", error));

 // پاک کردن فرم
  e.target.reset();
});