const arrFilterCategories=[]
const getCategories = async () => {
    try {
        const categories = await fetch(`https://localhost:44379/api/Categories`) //just api/Categories
        const allCategories = await categories.json();
        return allCategories
    }
    catch (error) {
        console.log(dataPost)
    }
}

const clearProductsHTML = () => {
   document.querySelector("#ProductList").innerHTML = ""
}

const showProductsCard = async () => {
    await clearProductsHTML()
    const products = await getProducts()
    if (products) {
        products?.forEach(product => {
            drawOneProduct(product)
        })
    }
        

}
const showCategories = async () => {
    const categories = await getCategories()
    if (categories) {
        categories.forEach(category => {
            drawOneCategory(category)
        })
    }


}
const addFilterCategory = (e, category) => {
    if (e) {
        arrFilterCategories.push(category.categoryId)
    }
    else {
        const indexCategory = arrFilterCategories.indexOf(category)
        arrFilterCategories.splice(indexCategory,1)
    }
    showProductsCard()
}

const drawOneCategory = (category) => {
    let tmp = document.getElementById('temp-category');
    let cloneCategory = tmp.content.cloneNode(true)
    cloneCategory.querySelector('.OptionName').innerText = category.categoryName
    cloneCategory.querySelector(".opt").addEventListener("change", (e) => addFilterCategory(e.currentTarget.checked,category))
    document.getElementById('categoryList').appendChild(cloneCategory)
}

const drawOneProduct = (product) => {
    let tmp = document.getElementById('temp-card');
    let cloneProduct = tmp.content.cloneNode(true)
    cloneProduct.querySelector('img').src = `./bags/${product.picture}`
    cloneProduct.querySelector('h1').textContent = product.productName
    cloneProduct.querySelector('.price').innerText = product.price
    cloneProduct.querySelector('.description').innerText = product.description
    cloneProduct.querySelector('button').addEventListener("click", () => { addToCart(product) })
    document.getElementById('ProductList').appendChild(cloneProduct)
}

const loadProducts = () => {
   window.addEventListener("load", () => {
        showProductsCard()
    })
}
const loadCategories = () => {
    window.addEventListener("load", () => {
        showCategories()
    })
}
loadProducts()
loadCategories()


const addToCart = (product) => {
    const orderList = JSON.parse(sessionStorage.getItem("orderList")) || []
    orderList.push(product)
    sessionStorage.setItem("orderList", JSON.stringify(orderList))
    document.getElementById("ItemsCountText").innerText = orderList.length
}

const getProducts = async () => {
    const filters = await getFilters()
    let url = `api/Products?nameSearch=${filters.nameSearch}&minPrice=${filters.minPrice}&maxPrice=${filters.maxPrice}`// check if each item has value, and then add it to url
    if (arrFilterCategories.length > 0)
        for (var i = 0; i < arrFilterCategories.length; i++) {// map is nicer
            url += `&categoryIds=${arrFilterCategories[i]}`
        }
        
  
    try {
        const products = await fetch(url, {
            method: 'Get',
            headers: {
                'content-Type': 'application/json'
            },
            query: { categoryIds: arrFilterCategories }
        })
        const allProducts = await products.json();
        console.log(allProducts)
        return allProducts
    }
    catch (error) {
        console.log(dataPost)
    }
}

const getFilters = async() => {
    const filters = {
        nameSearch:document.querySelector("#nameSearch").value,
        minPrice: document.getElementById("minPrice").value,
        maxPrice: document.getElementById("maxPrice").value
    }
    return filters
}