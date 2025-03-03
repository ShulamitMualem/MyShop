const getUserOrder = () => JSON.parse(sessionStorage.getItem("orderList")) || [];

const updateOrderList = (orderList) => {
    sessionStorage.setItem("orderList", JSON.stringify(orderList));
    showProductsCard();
    updateOrderSummary();
};

const deleteItem = (product) => {
    let orderList = getUserOrder();
    orderList = orderList.filter(item => item.productId !== product.productId);
    updateOrderList(orderList);
};

const drawOneProduct = (product) => {
    const template = document.getElementById("temp-row");
    const cloneProduct = template.content.cloneNode(true);
    cloneProduct.querySelector(".image").style.backgroundImage = `url(./images/${product.imgUrl})`;
    cloneProduct.querySelector(".itemName").textContent = product.productName;
    cloneProduct.querySelector(".itemNumber").innerText = product.price;
    cloneProduct.querySelector(".price").innerText = product.price;
    cloneProduct.querySelector(".DeleteButton").addEventListener("click", () => deleteItem(product));

    document.querySelector(".cistGroup").appendChild(cloneProduct);
};

const updateOrderSummary = () => {
    const orderList = getUserOrder();
    const totalAmount = orderList.reduce((sum, item) => sum + item.price, 0);
    document.getElementById("totalAmount").innerText = totalAmount;
    document.getElementById("itemCount").innerText = orderList.length;
};

const clearProductsHTML = () => {
    document.querySelector(".cistGroup").innerHTML = "";
};

const showProductsCard = () => {
    clearProductsHTML();
    getUserOrder().forEach(drawOneProduct);
};

const processPayment = async () => {
    const orderList = getUserOrder();
    const newOrder = await createOrder(orderList);

    if (newOrder) {
        alert(`הזמנתך מספר ${newOrder.orderId} התקבלה בהצלחה`);
        sessionStorage.removeItem("orderList");
        window.location.href = "Products.html";
    }
};

const initializeOrderPage = () => {
    window.addEventListener("load", () => {
        showProductsCard();
        updateOrderSummary();
    });
};

const createOrderData = (orderList) => ({
    orderSum: document.getElementById("totalAmount").textContent,
    userId: JSON.parse(sessionStorage.getItem("user"))?.userId,
    orderItems: orderList.map(item => ({ productId: item.productId, quantity: 1 })),
});

const createOrder = async (orderList) => {
    if (!sessionStorage.getItem("user")) {
        window.location.href = "Login.html";
        return null;
    }

    const orderData = createOrderData(orderList);

    try {
        const response = await fetch("api/Orders", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(orderData),
        });

        return response.ok ? await response.json() : null;
    } catch (error) {
        console.error("Error creating order:", error);
        alert("Error creating order")
        return null;
    }
};

initializeOrderPage();
