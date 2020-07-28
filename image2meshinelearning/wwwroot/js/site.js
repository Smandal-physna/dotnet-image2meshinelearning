//const uri = 'api/image2meshinelearning';
function addItem() {
    var files = document.getElementById('pic').files;
    var formData = new FormData();
    formData.append('file', files[0]);
    console.log(formData)
    let chunkPath = '/FileUpload/File';
    const uri = '/FileUpload/File';
    fetch(uri, {
        //url: chunkPath,
        enctype: "multipart/form-data",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false
    });
    //    .done(function (data) {
    //    // data is return information
    //});
    //fetch(uri, {
    //    //url: '/Image/File',
    //    url: '@Url.Action("File")',
    //    method: 'POST',
    //    body: formData,
    //    processData: false,
    //    contentType: false,
    //    success: function (result) {
    //    }
    //});
   }

    //fetch(uri, {
    //    method: 'POST',
    //    headers: {
    //        'Accept': 'application/json',
    //        'Content-Type': 'application/json; charset=utf-8'//'application/json'
    //    },
    //    body: item
    //})
    //    .then(response => response.json())
    //    .then(() => {
    //        getItems();
    //        addNameTextbox.value = '';
    //    })
    //        .catch(error => console.error('Unable to add item.', error));
