
async function login(password, serviceId) {
    try {
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");

        var raw = JSON.stringify({ "Password": password, "ServiceId": serviceId });

        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            body: raw,
            redirect: 'follow'
        };

        let result = await fetch("/api/v1/auth/login", requestOptions)
        if (result.status !== 200) {
            throw new Error(await result.json())
        }
        return await result.json();
    } catch (error) {
        throw error
    }
}

async function getListServices() {
    try {

        var requestOptions = {
            method: 'GET',
            redirect: 'follow'
        };

        let result = await fetch("/api/v1/services", requestOptions);
        if (result.status !== 200) {
            throw new Error("Lấy danh sách dịch vụ không thành công")
        }
        result = result.json()
        return result
    } catch (error) {
        alert(error.message)
    }
}

async function changeService(password, serviceId) {
    try {
        const result = await login(password, serviceId);
        window.localStorage.setItem('token', result.Token)
        const passCode = await getAuthen()
        window.localStorage.setItem('passCode', JSON.stringify(passCode))
        return result
    } catch (error) {
        throw error
    }
}


async function logOut(password) {
    try {
        let passCode = window.localStorage.getItem('passCode');
        if (!passCode) {
            return;
        }
        passCode = JSON.parse(passCode);
        await login(password, passCode.ServiceId)
        window.localStorage.removeItem("token")
        window.localStorage.removeItem("passCode")
    } catch (error) {
    console.log(error)
        throw error
    }
}

// get authen
async function getAuthen(inputToken) {
    try {
        const token = inputToken || window.localStorage.getItem('token');
        var myHeaders = new Headers();
        myHeaders.append("Authorization", token);

        var requestOptions = {
            method: 'GET',
            headers: myHeaders,
            redirect: 'follow'
        };

        let result = await fetch("/api/v1/auth/profile", requestOptions)
        if (result.status === 401) {
            throw new Error("Phiên đăng nhập không hợp lệ.")
        }
        if (result.status !== 200) {
            throw new Error(await result.json())
        }
        result = await result.json();
        // save to browser localstorage
        return result
    } catch (error) {
        throw error
    }
}

async function getDefinedScores() {
    try {

        var requestOptions = {
            method: 'GET',
            redirect: 'follow'
        };

        const result = await fetch("/api/v1/rating/score-values", requestOptions);
        if (result.status !== 200) {
            throw new Error(await result.json())
        }
        return await result.json()
    } catch (error) {
        throw error
    }
}

// submit review
async function handkeSubmitReview(scoreId, note) {
    try {
        const token = window.localStorage.getItem('token');
        var myHeaders = new Headers();
        myHeaders.append("Authorization", token);
        myHeaders.append("Content-Type", "application/x-www-form-urlencoded");

        var urlencoded = new URLSearchParams();
        urlencoded.append("ScoreId", scoreId);
        urlencoded.append("Note", note);

        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            body: urlencoded,
            redirect: 'follow'
        };

        let result = await fetch("/api/v1/rating/submit-review", requestOptions)
        if (result.status === 401) {
            window.location.href = '/Auth/Login'
            throw new Error("Phiên đăng nhập không hợp lệ.")
        }
        if (result.status !== 200) {
            throw new Error(await result.json())
        }
    } catch (error) {
        throw error
    }
}

$(document).ready(() => {
    getAuthen()
        .then(profile => {
            window.localStorage.setItem('passCode', JSON.stringify(profile))
        })
        .catch(error => {
            alert(error.message || error.message.Message)
            window.location.href = '/Auth/Login'
        })

    $('#log_out').click(() => {
        const password = $('#passwordLogout').val()
        logOut(password).then(success => {
            window.location.href = '/Auth/Login'
        }).catch(error => {
            $('#passwordLogoutHelpBlock').text(error.message)
        })
    })
})

