const getInputValue = (selector) => document.querySelector(selector)?.value.trim() || "";

const getAllDetailsForLogin = () => ({
    UserName: getInputValue("#userNameLogin"),
    Password: getInputValue("#passwordLogin"),
});

const getAllDetailsForSignUp = () => {
    const newUser = {
        UserName: getInputValue("#userName"),
        Password: getInputValue("#password"),
        FirstName: getInputValue("#firstName"),
        LastName: getInputValue("#lastName"),
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

const isValidUser = (user) => user.UserName && user.Password;

const checkPassword = async () => {
    const password = getInputValue("#password");
    const resultElement = document.querySelector("#checkPassword");

    try {
        const response = await fetch(`api/Users/password?password=${password}`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
        });

        const data = await response.json();
        resultElement.value = data;

        if (!response.ok) throw new Error("סיסמה לא חזקה");
    } catch (error) {
        alert(error.message);
    }
};

const addNewUser = async () => {
    const newUser = getAllDetailsForSignUp();
    if (!newUser) return;

    try {
        await checkPassword();

        const response = await fetch(`api/Users`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(newUser),
        });

        if (response.status === 400) throw new Error("!כל השדות חובה, בדוק את תקינותם");
        if(response.status === 409) throw new Error("שם משתמש כבר קיים")
        if (!response.ok) throw new Error("משהו השתבש, נסה שוב");
        alert("משתמש נןסף בהצלחה")
    } catch (error) {
        alert(error.message);
    }
};

const showRegister = () => document.querySelector('.signUpDiv').style.display = 'block';;

const login = async () => {
    const user = getAllDetailsForLogin();
    if (!isValidUser(user)) return alert("כל השדות חובה");

    try {
        const response = await fetch(`api/Users/login?userName=${user.UserName}&password=${user.Password}`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
        });

        if (response.status === 400) throw new Error("כל השדות חובה");
        if (response.status === 204) throw new Error("משתמש לא רשום");
        if (!response.ok) throw new Error("משהו השתבש, נסה שוב");

        const data = await response.json();
        // שמירת המשתמש בלבד, הטוקן נשמר ב-cookie ע"י השרת
        sessionStorage.setItem("user", JSON.stringify(data.user));
        window.location.href = "ShoppingBag.html";
    } catch (error) {
        alert(error.message);
    }
};
