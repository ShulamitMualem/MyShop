﻿const getInputValue = (selector) => document.querySelector(selector)?.value.trim() || "";

const setUpdatePageValues = () => {
    const currentUser = JSON.parse(sessionStorage.getItem("user"));
    if (!currentUser) return;

    document.querySelector("#userNameUpdate").value = currentUser.userName.trim();
    document.querySelector("#passwordUpdate").value = currentUser.password.trim();
    document.querySelector("#firstNameUpdate").value = currentUser.firstName.trim();
    document.querySelector("#lastNameUpdate").value = currentUser.lastName.trim();
};

const getUpdatedUserDetails = () => {
    const newUser = {
        UserName: getInputValue("#userNameUpdate"),
        Password: getInputValue("#passwordUpdate"),
        FirstName: getInputValue("#firstNameUpdate"),
        LastName: getInputValue("#lastNameUpdate"),
    };

    if (Object.values(newUser).some(value => !value)) {
        alert("כל השדות הם חובה. נא למלא את כולם.");
        return null;
    }

    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(newUser.UserName)) {
        alert("כתובת האימייל אינה תקינה.");
        return null;
    }

    if (newUser.FirstName.length > 20 || newUser.LastName.length > 20) {
        alert("שם פרטי ושם משפחה עד 20 תווים בלבד.");
        return null;
    }

    return newUser;
};
const checkIfUserExsist=()=>{
    const currentUser = JSON.parse(sessionStorage.getItem("user"));
    if (!currentUser) {
        alert("שגיאה: אין משתמש מחובר.");
        window.location.href = "Products.html";
    }
}
const updateUser = async () => {

    const updatedUser = getUpdatedUserDetails();
    if (!updatedUser) return;



    try {
        await checkPassword();
        debugger
        const currentUser = JSON.parse(sessionStorage.getItem("user"))
        const response = await fetchWithAuth(`api/Users/${currentUser.userId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(updatedUser),
        });
        if (response === null) return; // נשלח ל-login אם לא מורשה
        if (response.status === 400) throw new Error("!כל השדות חובה, בדוק את תקינותם");
        if (response.status === 409) throw new Error("שם משתמש כבר קיים")
        if (!response.ok) throw new Error("משהו השתבש, נסה שוב");

        alert(`פרטי משתמש ${currentUser.userId} עודכנו בהצלחה!`);
        sessionStorage.setItem("user", JSON.stringify(await response.json()));
        window.location.href = "Products.html";
    } catch (error) {
        alert(error.message);
    }
};

const checkPassword = async () => {
    const password = getInputValue("#passwordUpdate");
    const resultElement = document.querySelector("#checkPassword");

    try {
        const response = await fetch(`api/Users/password?password=${password}`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
        });

        const data = await response.json();
        resultElement.value = data;

        if (!response.ok) {
            throw new Error("סיסמה לא חזקה");
        }
    } catch (error) {
        throw error;
    }
};

setUpdatePageValues();
checkIfUserExsist()
