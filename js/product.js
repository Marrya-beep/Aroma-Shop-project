document.addEventListener("DOMContentLoaded", function () {
// برای سرچ با input
  const searchInput = document.querySelector(".filter-bar-search input");

  if (!searchInput) return;

  let debounceTimeout;

  searchInput.addEventListener("input", () => {
    clearTimeout(debounceTimeout);
    debounceTimeout = setTimeout(() => {
      getProductInfo(searchInput.value);
    }, 300); // صبر 300ms بعد از تایپ
  });

  // نمایش اولیه همه محصولات
  getProductInfo();
});


function getProductInfo(query = "") {
  
  const containerProduct = document.getElementById("container_products");
  if (!containerProduct) return;

  containerProduct.innerHTML = "";

  fetch("https://localhost:7097/api/Show-Item")
    .then(response => {
      if (!response.ok) {
        throw new Error("HTTP Error: " + response.status);
      }
      return response.json();
    })
    .then(products => {
      const q = query.trim().toLowerCase();

      const filteredProducts = products.filter(product =>
        product.name.toLowerCase().includes(q) ||
        product.category.toLowerCase().includes(q)
      );

      if (!filteredProducts.length) {
        containerProduct.innerHTML = `
          <div class="col-12 text-center">
            <p>هیچ محصولی پیدا نشد</p>
          </div>`;
        return;
      }

      filteredProducts.forEach(product => {
        const col = document.createElement("div");
        col.className = "col-md-6 col-lg-4";

        col.innerHTML = `
          <div class="card text-center card-product">
            <div class="card-product__img">
              <img
                class="card-img"
                src="${product.image || 'img/product/product1.png'}"
                alt="${product.name}"
              />
              <ul class="card-product__imgOverlay">
                <li>
                  <button title="مشاهده">
                    <i class="ti-search"></i>
                  </button>
                </li>
                <li>
                  <button title="افزودن به سبد">
                    <i class="ti-shopping-cart"></i>
                  </button>
                </li>
                <li>
                  <button title="علاقه‌مندی">
                    <i class="ti-heart"></i>
                  </button>
                </li>
              </ul>
            </div>

            <div class="card-body">
              <p>${product.category}</p>
              <h4 class="card-product__title">
                <a href="#">${product.name}</a>
              </h4>
              <p class="card-product__price">
                ${Number(product.price).toLocaleString()} تومان
              </p>
            </div>
          </div>
        `;

        containerProduct.appendChild(col);
      });
    })
    .catch(error => {
      console.error(error);
      containerProduct.innerHTML = `
        <div class="col-12 text-center">
          <p>خطا در دریافت محصولات</p>
        </div>`;
    });
}




