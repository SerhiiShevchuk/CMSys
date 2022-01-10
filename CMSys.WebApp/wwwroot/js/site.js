console.log("jsWork!");

var routAPI = document.getElementById("routAPI");

function sort() {
    let nodeList = document.querySelectorAll('.row-info');
    nodeList = Array.from(nodeList)

    let sortedRows = nodeList.sort((rowA, rowB) => parseInt(rowA.querySelector('div:nth-child(3)').innerHTML) > parseInt(rowB.querySelector('div:nth-child(3)').innerHTML)  ? 1 : -1)
    $('.main-page > .container').append(...sortedRows);
}

$("#createGroup").click(function (event) {
    event.preventDefault();

    let body = {
        Name: $('#Name').val(),
        VisualOrder: $("#VisualOrder").val(),
        Description: $("#Description").val(),
    }

    $.ajax({
        url: `https://localhost:5001/api/${routAPI.innerHTML}/create`,
        type: 'POST',
        data: JSON.stringify(body),
        contentType: "application/json;charset=utf-8",
        success: function (data, status) {
            console.log(data)

            $("[data-valmsg-for='Name']").empty()
            $("[data-valmsg-for='VisualOrder']").empty()
            $("[data-valmsg-for='Description']").empty()

            $('.main-page > .container').append(
                `<div class="row row-info align-items-center fs-6" data-group-id="${data.id}">
                <div class= "col-5">${data.name}</div >
                <div class="col-3">${data.description}</div>
                <div class="col">${data.visualOrder}</div>
                <div class="col">
                    <div class="action-wrap">
                        <div class="row">
                            <a class="action-yellow flex-center" data-bs-toggle="modal" data-bs-target="#t${data.id}"><i class="fas fa-pencil-alt"></i></a>
                            <a id="deleteGroup" class="action-red flex-center" data-group-id="${data.id}"><i class="fas fa-trash-alt"></i></a>

                            <div class="modal fade" id="t${data.id}" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title text4" id="exampleModalLabel">Create Group</h5>
                                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                        </div>
                                        <div class="modal-body">
                                            <form data-group-id="${data.id}">
                                                <div class="mb-2" hidden="">
                                                    <label class="col-form-label text4" for="group_Id">${data.id}</label>
                                                    <input class="form-control text3" type="text" data-val="true" data-val-required="The Id field is required." id="group_Id" name="group.Id" value="${data.id}">
                                                </div>
                                                <div class="mb-2">
                                                    <label class="col-form-label text4" for="group_Name">Name</label>
                                                    <input class="form-control text3" type="text" id="group_Name" name="group.Name" value="${data.name}">
                                                    <span class="text-error field-validation-valid" data-valmsg-for="group.Name" data-valmsg-replace="true"></span>
                                                </div>
                                                <div class="mb-2">
                                                    <label class="col-form-label text4" for="group_VisualOrder">Order</label>
                                                    <input class="form-control text3" type="number" data-val="true" data-val-required="The VisualOrder field is required." id="group_VisualOrder" name="group.VisualOrder" value="${data.visualOrder}">
                                                    <span class="text-error field-validation-valid" data-valmsg-for="group.VisualOrder" data-valmsg-replace="true"></span>
                                                </div>
                                                <div class="mb-2">
                                                    <label class="col-form-label text4" for="group_Description">Description</label>
                                                    <textarea class="form-control text3" id="group_Description" name="group.Description">${data.description}</textarea>
                                                    <span class="text-error field-validation-valid" data-valmsg-for="group.Description" data-valmsg-replace="true"></span>
                                                </div>
                                            </form>
                                        </div>
                                        <div class="modal-footer">
                                            <button id="updateGroup" data-group-id="${data.id}" type="button" class="btn btn-primary">Update</button>
                                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
           `);
            subscribeDeleteBtns()
            subscribeUpdateBtns();
            sort();
        },
        error: function (jxqr, error, status) {
            // парсинг json-объекта
            var response = jQuery.parseJSON(jxqr.responseText);

            console.log(response)

            $("[data-valmsg-for='Name']").empty()
            $("[data-valmsg-for='VisualOrder']").empty()
            $("[data-valmsg-for='Description']").empty()

            if (response['errors']['Name']) {

                $.each(response['errors']['Name'], function (index, item) {
                    $("[data-valmsg-for='Name']").text(item)
                });
            }
            if (response['errors']['VisualOrder']) {

                $.each(response['errors']['VisualOrder'], function (index, item) {
                    $("[data-valmsg-for='VisualOrder']").text(item)
                });
            }
            if (response['errors']['Description']) {
                $.each(response['errors']['Description'], function (index, item) {
                    $("[data-valmsg-for='Description']").text(item)
                });
            }
        }
    });
})


function subscribeUpdateBtns() {
    var updateBtns = document.querySelectorAll('#updateGroup')

    updateBtns.forEach(function (button) {
        $(button).click(function (event) {
            var groupId = this.dataset.groupId
            console.log('UpdateId: ' + groupId)

            let form = document.querySelector(`form[data-group-id = '${groupId}']`)
            let row = document.querySelector(`.row[data-group-id = '${groupId}']`)
            console.log(row)
            console.log(form)

            console.log('id: ' + $(form.querySelector('#group_Id')).val())

            let body = {
                Id: $(form.querySelector('#group_Id')).val(),
                Name: $(form.querySelector('#group_Name')).val(),
                VisualOrder: $(form.querySelector('#group_VisualOrder')).val(),
                Description: $(form.querySelector('#group_Description')).val(),
            }
            console.log(body)


            $.ajax({
                url: `https://localhost:5001/api/${routAPI.innerHTML}/update`,
                type: 'PUT',
                data: JSON.stringify(body),
                contentType: "application/json;charset=utf-8",
                success: function (data, status) {
                    console.log(data)

                    $(form.querySelector("[data-valmsg-for='group.Name']")).empty();
                    $(form.querySelector("[data-valmsg-for='group.VisualOrder']")).empty();
                    $(form.querySelector("[data-valmsg-for='group.Description']")).empty();

                    $(row.querySelector("div:nth-child(1)")).text(data.name);
                    $(row.querySelector("div:nth-child(2)")).text(data.description);
                    $(row.querySelector("div:nth-child(3)")).text(data.visualOrder);

                    sort();
                },
                error: function (jxqr, error, status) {
                    // парсинг json-объекта
                    var response = jQuery.parseJSON(jxqr.responseText);

                    console.log('update')
                    console.log(response)

                    $(form.querySelector("[data-valmsg-for='group.Name']")).empty()
                    $(form.querySelector("[data-valmsg-for='group.VisualOrder']")).empty()
                    $(form.querySelector("[data-valmsg-for='group.Description']")).empty()

                    if (response['errors']['Name']) {

                        $.each(response['errors']['Name'], function (index, item) {

                            $(form.querySelector("[data-valmsg-for='group.Name']")).text(item)
                        });
                    }
                    if (response['errors']['VisualOrder']) {

                        $.each(response['errors']['VisualOrder'], function (index, item) {

                            $(form.querySelector("[data-valmsg-for='group.VisualOrder']")).text(item)
                        });
                    }
                    if (response['errors']['Description']) {

                        $.each(response['errors']['Description'], function (index, item) {

                            $(form.querySelector("[data-valmsg-for='group.Description']")).text(item)
                        });
                    }
                }
            });
        });
    });

}


function subscribeDeleteBtns() {
    var deleteBtns = document.querySelectorAll('#deleteGroup')

    deleteBtns.forEach(function (button) {
        $(button).click(function (event) {

            var groupId = this.dataset.groupId
            console.log('DeleteId: ' + groupId)

            let row = document.querySelector(`.row[data-group-id = '${groupId}']`)

            $.ajax({
                url: `https://localhost:5001/api/${routAPI.innerHTML}/delete/${groupId}`,
                type: 'Delete',
                data: JSON.stringify(groupId),
                contentType: "application/json;charset=utf-8",
                success: function (data, status) {
                    console.log(data)
                    row.remove();
                },
                error: function (jxqr, error, status) {
                    // парсинг json-объекта
                    console.log('badRequest')
                    console.log($('#errorWindow'))
                    $('#errorWindow').modal('show')
                }
            });
        });
    });

}

subscribeUpdateBtns()
subscribeDeleteBtns()