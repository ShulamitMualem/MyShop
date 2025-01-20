const getUserOrder = () => {
    const orderList = JSON.parse(sessionStorage.getItem("orderList")) || [];
    return orderList;
}
const deleteItem = (product) => {
    let orderList = getUserOrder()
    for (let i = 0; i < orderList.length; i++) {
        if (orderList[i].productId == product.productId) {
            orderList.splice(i,1)
            break;
        }
      
    }

    sessionStorage.setItem("orderList", JSON.stringify(orderList))
    showProductsCard()
    calculateCountAndAmount()
}
const drawOneProduct = (product) => {
    let tmp = document.getElementById('temp-row');
    let cloneProduct = tmp.content.cloneNode(true)
    cloneProduct.querySelector('.image').style.backgrounImage = `url(${product.imgUrl})`   
    cloneProduct.querySelector('.itemName').textContent = product.productName
    cloneProduct.querySelector('.itemNumber').innerText = product.price
    cloneProduct.querySelector('.price').innerText = product.price
    cloneProduct.querySelector('.DeleteButton').addEventListener("click", () => { deleteItem(product) })
    document.querySelector('.cistGroup').appendChild(cloneProduct)
}
const calculateCountAndAmount = () => {
    const orderList = getUserOrder()
    let totalAmount = 0
    orderList.map(item =>totalAmount+= item.price)

    document.getElementById("totalAmount").innerText = totalAmount
    document.getElementById("itemCount").innerText = orderList.length
}
const clearProductsHTML = () => {
    document.querySelector(".cistGroup").innerHTML = ""
}
const showProductsCard = () => {
    clearProductsHTML()
    const orderList =  getUserOrder()
    if (orderList) {
        orderList?.forEach(orderItem => {
            drawOneProduct(orderItem)
        })
    }
}
const payment = async () => {
    const orderList = getUserOrder()
    const toClearseSsionStorage = await createOrder(orderList)
    if (toClearseSsionStorage) {
        sessionStorage.removeItem("orderList")
        window.location.href='Products.html'
    }
}
const loadOrderList = () => {
    window.addEventListener("load", () => {
        showProductsCard()
    })
}
loadOrderList()
calculateCountAndAmount()
const createOrderPost = (listOrderItem) => {
   return orderPost = {
       orderSum: document.getElementById("totalAmount").textContent,
       userId: JSON.parse(sessionStorage.getItem("user")).userId,
       orderItem: listOrderItem.map(item => { return { productId: item.productId, quantity:1 } })
    }
}
const createOrder = async (orderList) => {
    if (!sessionStorage.getItem("user")) {
        window.location.href = 'Login.html'

        return null
    }

    const createOrder = createOrderPost(orderList)
    try {
        const order = await fetch('api/Orders', {
            method: 'Post',
            headers: {
                'content-Type': 'application/json'
            },
          
                body: JSON.stringify(createOrder)
            
        })
        const newOrder = await order.json();
        console.log(newOrder)
        return newOrder
    }
    catch (error) {
        console.log(dataPost)
    }
}