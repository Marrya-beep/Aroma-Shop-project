let products = [];
// /// اضافه کردن محصول
document
  .getElementById("addProductForm")
  .addEventListener("submit", function (e) {
    e.preventDefault();

    // گرفتن مقادیر فرم
    let nameProduct = document.getElementById("nameProduct").value;
    let price = document.getElementById("price").value;
    let category = document.getElementById("category").value;
    let description = document.getElementById("discription").value;

    // ساخت شیء محصول
    let product = {
      name: nameProduct,
      price: price,
      category: category,
      description: description,
    };

    products.push(product);
   console.log(products)
    fetch("http://localhost:5013/api/AddItem", {
      method: "POST",
      headers: {
        "Content-Type": "application/json", // فرستادن JSON
      },
      body: JSON.stringify(product), // فقط محصول جدید ارسال می‌شود
    })
      .then((response) => response.text()) // یا response.json() اگر JSON برگرده
      .then((data) => {
        console.log("پاسخ سرور:", data);
      })
      .catch((error) => console.error("خطا در ارسال:", error));

    // پاک کردن فرم
    e.target.reset();
  });