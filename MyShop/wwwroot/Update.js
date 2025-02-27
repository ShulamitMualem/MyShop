
const editValueOfUpdatePage = () => {
    const userName = document.querySelector("#userNameUpdate")
    const password = document.querySelector("#passwordUpdate")
    const firstName = document.querySelector("#firstNameUpdate")
    const lastName = document.querySelector("#lastNameUpdate")
    const currentUser = JSON.parse(sessionStorage.getItem("user"))
    userName.value = currentUser.userName
    password.value = currentUser.password
    firstName.value = currentUser.firstName
    lastName.value = currentUser.lastName
}
editValueOfUpdatePage()
const getAllDetilesForUpdate = () => {
    return newUser = {
        UserName: document.querySelector("#userNameUpdate").value,
        Password: document.querySelector("#passwordUpdate").value,
        FirstName: document.querySelector("#firstNameUpdate").value,
        LastName: document.querySelector("#lastNameUpdate").value
    }
}
const updateUser = async () => {
    const updateUser = getAllDetilesForUpdate()
    const currentUser = JSON.parse(sessionStorage.getItem("user"))
    try {
        await checkPassword()
        const responseput = await fetch(`https://localhost:44379/api/Users/${currentUser.userId}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(updateUser)
        })
        if (!responseput.ok)
            throw new Error(`HTTP error! status ${responsePost.status}`)


        if (responseput.status == 200) {
            alert(`פרטי משתמש ${currentUser.userId} עודכנו בהצלחה!`)
            sessionStorage.setItem("user", JSON.stringify(await responseput.json()))
            window.location.href = "HomePage.html"
        }

    }
    catch (error) {
        alert("מצטערים משהו השתשב נסה שוב...\nהשגיאה:"+error)
    }

}
const checkPassword = async () => {
    const password = document.querySelector("#passwordUpdate").value
    let result = document.querySelector("#checkPassword")
    try {
        const responsePost = await fetch(`https://localhost:44379/api/Users/password?password=${password}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
        })
        if (!responsePost.ok) {
            const dataPost = await responsePost.json();
            console.log(dataPost)
            result.value = dataPost
            throw new Error("סיסמה לא חזקה")
        }
        const dataPost = await responsePost.json();
        console.log(dataPost)
        result.value = dataPost

    }
    catch (error) {
        throw (error)
    }
}