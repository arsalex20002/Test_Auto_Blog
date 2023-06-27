document.addEventListener("DOMContentLoaded", function () {
    var descriptionElements = document.querySelectorAll("[id^='description']");

    descriptionElements.forEach(function (element) {
        var descriptionText = element.textContent;

        if (descriptionText.length > 500) {
            var trimmedText = descriptionText.substring(0, 500);
            var lastPeriodIndex = trimmedText.lastIndexOf(".");

            if (lastPeriodIndex !== -1) {
                trimmedText = trimmedText.substring(0, lastPeriodIndex) + "...";
            } else {
                trimmedText += "...";
            }

            element.textContent = trimmedText;
        }
    });

    $(document).ready(function () {
        // �������� ������ ������� �����
        $.ajax({
            url: '/Car/GetCarModels', // URL-����� �������� �����������, ������������� ������ �������
            type: 'GET',
            success: function (models) {

                // ���������� ������ ������ � <select> � ���� �����
                $.each(models, function (index, model) {
                    $('#modelSelect').append($('<option></option>').val(model).text(model));
                });
            },
            error: function () {
                // ��������� ������ ��� �������� ������ �������
                console.log('������ �������� ������ ������� �����');
            }
        });
    });
    $(document).ready(function () {
        // ��������� �������� TypeCar �� URL
        var urlParams = new URLSearchParams(window.location.search);
        var typeCar = urlParams.get('TypeCar');

        // ��������� �������� TypeCar � ���������� ������
        $('#modelSelectType').val(typeCar);
    });
});