var currentSelect = 1;
$(document).ready(function () {
    function getCurrentPassCode() {
        const passCode = window.localStorage.getItem('passCode')
        console.log(passCode)
        if (!passCode) {
            return;
        }
        return JSON.parse(passCode)
    }
    const passCode = getCurrentPassCode()
    renderServices(passCode)

    $('#change_service').click(() => {
        console.log(passCode.ServiceId, currentSelect)
        if (currentSelect === passCode.ServiceId) {
            $('#change_service_password_help').text('Vui lòng chọn 1 dịch vụ khác để đổi')
            return;
        }


        const password = $('#change_service_password').val()
        changeService(password, currentSelect)
            .then(success => {
                $('#change_service_password_help').text('')
                window.location.reload()
            }).catch(error => {
                $('#change_service_password_help').text('Vui lòng kiểm tra mật khẩu')
            })
    })
});

async function renderServices(passCode) {
    const services = await getListServices()
    
    const listServices = $("#modal_list_services");
    currentSelect = passCode.ServiceId;
    services.forEach((data) => {
        listServices.append(
            `
            <div id="modal_service_${data.Id}" class="col-4">
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

        $(`#modal_service_${data.Id}`).click(() => {
            clearCurrentSelect();
            currentSelect = data.Id;
            autoSelect();
        });
    });

    defaultService();

    function clearCurrentSelect() {
        $(`#modal_service_${currentSelect} div`).removeClass(
            "bg-info text-white"
        );
    }
    function autoSelect() {
        $(`#modal_service_${currentSelect} div`).addClass(
            "bg-info text-white"
        );
    }

    function defaultService() {
        $(`#modal_service_${currentSelect} div`).addClass(
            "bg-secondary text-white"
        );
    }
}