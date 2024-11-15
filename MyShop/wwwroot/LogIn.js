
const getAllDetilesForLogin = () => {
    return user = {
        UserName: document.querySelector("#userNameLogin").value,
        Password: document.querySelector("#passwordLogin").value,
    }
}
const getAllDetilesForSignUp = () => {
    return newUser = {
        UserName: document.querySelector("#userName").value,
        Password: document.querySelector("#password").value,
        FirstName: document.querySelector("#firstName").value,
        LastName: document.querySelector("#lastName").value
    }
}

const checkData = (user) => {
    return (user.UserName&&user.Password)
}
const checkPassword = async () => {
    const password = document.querySelector("#password").value
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
const addNewUser = async () => {

    const newUser = getAllDetilesForSignUp()
   
    try {
       await checkPassword()
        const responsePost = await fetch(`https://localhost:44379/api/Users`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newUser)
        })
            if (responsePost.status == 400)
            throw new Error("כל השדות חובה, בדוק את תקינותם")

            if (!responsePost.ok)
            throw new Error("משהו השתבש נסה שוב")

            const postData = responsePost.json()
       
    }
    catch (error) {
        alert( error)
    }


}
const showRegister = () => {
    const divREgister = document.querySelector(".signUpDiv")
    divREgister.classList.remove("signUpDiv")
}
const Login = async () => {
    const newUser = getAllDetilesForLogin()
    try { 
    const responsePost = await fetch(`https://localhost:44379/api/Users/login?userName=${newUser.UserName}&password=${newUser.Password}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        }

    })
    if (responsePost.status == 400)
        throw new Error("כל השדות חובה")
    if (responsePost.status == 204)
        throw new Error("משתמש לא רשום")
    if (!responsePost.ok)
        throw new Error("משהו השתבש נסה שוב")

        const dataPost = await responsePost.json();
        sessionStorage.setItem("user", JSON.stringify(dataPost))
        console.log(dataPost)
        window.location.href = "Update.html"
    }
    catch (error) {
        alert(error)
    }




}



