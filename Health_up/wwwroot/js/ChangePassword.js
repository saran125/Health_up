var password = document.getElementById("password")
    , confirm_password = document.getElementById("confirmPassword");

document.getElementById('signupLogo').src = "https://s3-us-west-2.amazonaws.com/shipsy-public-assets/shipsy/SHIPSY_LOGO_BIRD_BLUE.png";
enableSubmitButton();

function validatePassword() {
    if (password.value != confirm_password.value) {
        confirm_password.setCustomValidity("Passwords Don't Match");
        return false;
    } else {
        confirm_password.setCustomValidity('');
        return true;
    }
}

password.onchange = validatePassword;
confirm_password.onkeyup = validatePassword;

function enableSubmitButton() {
    document.getElementById('submitButton').disabled = false;
    document.getElementById('loader').style.display = 'none';
}

function disableSubmitButton() {
    document.getElementById('submitButton').disabled = true;
    document.getElementById('loader').style.display = 'unset';
}

function validateSignupForm() {
    var form = document.getElementById('signupForm');

    for (var i = 0; i < form.elements.length; i++) {
        if (form.elements[i].value === '' && form.elements[i].hasAttribute('required')) {
            console.log('There are some required fields!');
            return false;
        }
    }

    if (!validatePassword()) {
        return false;
    }

    onSignup();
}

function onSignup() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {

        disableSubmitButton();

        if (this.readyState == 4 && this.status == 200) {
            enableSubmitButton();
        }
        else {
            console.log('AJAX call failed!');
            setTimeout(function () {
                enableSubmitButton();
            }, 1000);
        }

    };

    xhttp.open("GET", "ajax_info.txt", true);
    xhttp.send();
}
function validate() {
    var str = document.getElementById("psw").value;
    if (str.length < 8) {
        document.getElementById("pwd_validate").innerHTML = "Password is very weak and must contain at least 8 characters";
        document.getElementById("pwd_validate").style.color = "Red";
        return ("no_number");
    }
    else if (str.search(/[0-9]/) == -1) {
        document.getElementById("pwd_validate").innerHTML = "Password is very weak and requires at 1 number";
        document.getElementById("pwd_validate").style.color = "Red";
        return ("no_number");
    }
    else if (str.search(/[a-z]/) == -1) {
        document.getElementById("pwd_validate").innerHTML = "Password is weak and requires at 1 lower-case letter";
        document.getElementById("pwd_validate").style.color = "Red";
        return ("no_number");
    }
    else if (str.search(/[A-Z]/) == -1) {
        document.getElementById("pwd_validate").innerHTML = "Password is medium and requires at 1 upper-case letter";
        document.getElementById("pwd_validate").style.color = "#FF4500";
        return ("no_number");
    }
    else if (str.search(/[@$!%*?&]/) == -1) {
        document.getElementById("pwd_validate").innerHTML = "Password is medium and requires at 1 special character";
        document.getElementById("pwd_validate").style.color = "#FF4500";
        return ("no_number");
    }
    else {
        document.getElementById("pwd_validate").innerHTML = "Password is strong";
        document.getElementById("pwd_validate").style.color = "Blue";
    }
}
function validate1() {
    var str = document.getElementById("psw1").value;
    if (str.length < 8) {
        document.getElementById("pwd_validate1").innerHTML = "Password is very weak and must contain at least 8 characters";
        document.getElementById("pwd_validate1").style.color = "Red";
        return ("less than 12");
    }
    else if (str.search(/[0-9]/) == -1) {
        document.getElementById("pwd_validate1").innerHTML = "Password is very weak and requires at 1 number";
        document.getElementById("pwd_validate1").style.color = "Red";
        return ("no_number");
    }
    else if (str.search(/[a-z]/) == -1) {
        document.getElementById("pwd_validate1").innerHTML = "Password is weak and requires at 1 lower-case letter";
        document.getElementById("pwd_validate").style.color = "Red";
        return ("no_lower");
    }
    else if (str.search(/[A-Z]/) == -1) {
        document.getElementById("pwd_validate1").innerHTML = "Password is medium and requires at 1 upper-case letter";
        document.getElementById("pwd_validate1").style.color = "#FF4500";
        return ("no_upper");
    }
    else if (str.search(/[@$!%*?&]/) == -1) {
        document.getElementById("pwd_validate1").innerHTML = "Password is medium and requires at 1 special character";
        document.getElementById("pwd_validate1").style.color = "#FF4500";
        return ("no_special");
    }
    else if (str != document.getElementById("psw").value) {
        document.getElementById("pwd_validate1").innerHTML = "Both Passwords are not same";
        document.getElementById("pwd_validate1").style.color = "#FF4500";
        return ("no_same");
    }
    else {
        document.getElementById("pwd_validate1").innerHTML = "Password is strong and confirmed";
        document.getElementById("pwd_validate1").style.color = "Blue";
    }
}
function validate2() {
    var str = document.getElementById("card").value;
    if (str.length != 16) {
        document.getElementById("card_validate").innerHTML = "Credit Card number must be 16 digits";
        document.getElementById("card_validate").style.color = "Red";
        return ("less than 12");
    }

    else {
        document.getElementById("card_validate").innerHTML = "Credit card number is valid";
        document.getElementById("card_validate").style.color = "Blue";
    }

}