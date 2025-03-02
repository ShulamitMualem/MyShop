const arrFilterCategories = [];

const getCategories = async () => {
    try {
        const response = await fetch('api/Categories');
        return await response.json();
    } catch (error) {
        console.error('שגיאה בהבאת קטגוריות:', error);
    }
};

const clearProductsHTML = () => {
    document.querySelector("#ProductList").innerHTML = "";
};

const showProductsCard = async () => {
    await clearProductsHTML();
    const products = await getProducts();
    if (products) {
        products.forEach(drawOneProduct);
    }
};

const showCategories = async () => {
    const categories = await getCategories();
    if (categories) {
        categories.forEach(drawOneCategory);
    }
};

const addFilterCategory = (e, category) => {
    if (e) {
        arrFilterCategories.push(category.categoryId);
    } else {
        const indexCategory = arrFilterCategories.indexOf(category.categoryId);
        if (indexCategory !== -1) {
            arrFilterCategories.splice(indexCategory, 1);
        }
    }
    showProductsCard();
};

const drawOneCategory = (category) => {
    const template = document.getElementById('temp-category');
    const clone = template.content.cloneNode(true);
    clone.querySelector('.OptionName').innerText = category.categoryName;
    clone.querySelector(".opt").addEventListener("change", (e) => addFilterCategory(e.currentTarget.checked, category));
    document.getElementById('categoryList').appendChild(clone);
};

const drawOneProduct = (product) => {
    const template = document.getElementById('temp-card');
    const clone = template.content.cloneNode(true);
    clone.querySelector('img').src = `./bags/${product.picture}`;
    clone.querySelector('h1').textContent = product.productName;
    clone.querySelector('.price').innerText = product.price;
    clone.querySelector('.description').innerText = product.description;
    clone.querySelector('button').addEventListener("click", () => addToCart(product));
    document.getElementById('ProductList').appendChild(clone);
};

const loadProducts = () => {
    window.addEventListener("load", showProductsCard);
};

const loadCategories = () => {
    window.addEventListener("load", showCategories);
};

loadProducts();
loadCategories();

const addToCart = (product) => {
    const orderList = JSON.parse(sessionStorage.getItem("orderList")) || [];
    orderList.push(product);
    sessionStorage.setItem("orderList", JSON.stringify(orderList));
    document.getElementById("ItemsCountText").innerText = orderList.length;
};

const getUrlForGetProducts = async () => {
    const filters = await getFilters();
    const baseUrl = `api/Products?`;
    const queryParams = [];

    if (filters.nameSearch) queryParams.push(`nameSearch=${encodeURIComponent(filters.nameSearch)}`);
    if (filters.minPrice) queryParams.push(`minPrice=${filters.minPrice}`);
    if (filters.maxPrice) queryParams.push(`maxPrice=${filters.maxPrice}`);
    if (arrFilterCategories.length > 0) {
        arrFilterCategories.forEach(categoryId => {
            queryParams.push(`categoryIds=${categoryId}`);
        });
    }

    return baseUrl + queryParams.join("&");
};

const getProducts = async () => {
    const url = await getUrlForGetProducts();
    try {
        const response = await fetch(url, { method: 'GET', headers: { 'Content-Type': 'application/json' } });
        const products = await response.json();
        document.getElementById("counter").innerText = products.length;
        return products;
    } catch (error) {
        console.error('שגיאה בהבאת מוצרים:', error);
    }
};

const getFilters = async () => {
    return {
        nameSearch: document.querySelector("#nameSearch").value,
        minPrice: document.getElementById("minPrice").value,
        maxPrice: document.getElementById("maxPrice").value
    };
};
