// get authen
async function getAuthenLoginPage(inputToken) {
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
        console.log(result)
        return result
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

        const result = await fetch("/api/v1/services", requestOptions).then(response => response.json());
        return result
    } catch (error) {
        alert(error.message)
    }
}


async function submitLogin(password, serviceId) {
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
        result = await result.json();
        // save to browser localstorage
        window.localStorage.setItem("token", result.Token);
        // set passCode to browser localstorage
        const passCode = await getAuthenLoginPage(result.Token);
        window.localStorage.setItem('passCode', JSON.stringify(passCode))
        return result
    } catch (error) {
        throw error
    }
}

var currentSelect = 1;
$(document).ready(() => {
    $('#submitLogin').addClass('disabled')
    const listServices = $("#list_services");

    getAuthenLoginPage().then(authenticated => {
        window.localStorage.setItem("passCode", JSON.stringify(authenticated))
        window.location.href = '/Home/Rating'
    }).catch(() => {})

    getListServices().then(services => {
        services.forEach((data) => {
            listServices.append(
                `
            <div id="service_${data.Id}" class="col-12 col-sm-6 col-lg-4">
                        <div class="card border-primary" style="height: 100%">
                          <div
                            class="card-body text-primary text-center d-flex justify-content-center text-danger"
                          >
                            <div class="align-self-center">
                              <div class="card-title">
                                <img
                                  style="width: 70px"
                                  src="${data.Icon}"
                                />
                              </div>
                              <p class="card-text"><b>${data.Name}</b></p>
                            </div>
                          </div>
                        </div>
            </div>
            `
            );

            $(`#service_${data.Id}`).click(() => {
                clearCurrentSelect();
                currentSelect = data.Id;
                autoSelect();
            });
            autoSelect();
            setTimeout(() => {
                $('#submitLogin').removeClass('disabled')
            }, 3000)
        });
    }).then(async () => {
        // submit login
        $('#submitLogin').click(() => {
            const password = $('#password').val()
            submitLogin(password, currentSelect).then(v => {
                // redirect to review page
                window.location.href = '/Home/Rating'
            }).catch(error => {
                $('#passwordHelpBlock').text("Vui lòng kiểm tra lại mật khẩu");
            })
        })
    })
});

function clearCurrentSelect() {
    $(`#service_${currentSelect} div`).removeClass("bg-info text-white");
}
function autoSelect() {
    $(`#service_${currentSelect} div`).addClass("bg-info text-white");
}