$().ready(function () {

    $("#regform").validate({
        rules: {
            Firstname: {
                required: true,
                minlength: 4,
                maxlength: 20,
                pattern: /^[а-яА-Яa-zA-Z0-9]+$/
            },
            Lastname: {
                required: true,
                minlength: 2,
                maxlength: 20,
                pattern: /^[а-яА-Яa-zA-Z0-9]+$/
            },
            Sex: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            Bthd: {
                pattern: /([0-2]\d|3[01])\.(0\d|1[012])\.(\d{4})/,
                required: true
            },
            Genres: {
                required: true
            },
            Books: {
                required: true
            },
        },
        messages: {
            Firstname: {
                required: "Введите имя!",
                minlength: "Имя должно быть больше 4 символов",
                maxlength: "Максимальное число символов: 20",
                pattern: "Имя состоит только из букв",
            },
            Lastname: {
                required: "Введите фамилию",
                minlength: "Минимальное число символов: 2",
                maxlength: "Максимальное число символов: 20",
                pattern: "Фамилия состоит только из букв",
            },
            Email: {
                required: "Введите Email!",
                email: "Email должен быть в формате example@ex.com",
            },
            Bthd: {
                pattern: "Дата вводится в формате дд.мм.гггг",
                required: "Введите дату рождения!",
            },
            Genres: {
                required: "Выберите хотя бы один жанр!",
            },
            Books: {
                required: "Выберите хоты бы одно предпочтение!",
            },

        },
        errorContainer: $('#errorContainer'),


    })

});