Feature: Авторизация
    Scenario: посещение главной страницы
        When Я посещаю 'http://localhost:4200/'
        Then Меня переводит на страницу авторизации 'http://localhost:4200/login'
        And Я вижу поля для ввода логина и пароля

    Scenario: авторизация на сайте
        When Я посещаю 'http://localhost:4200/login'
        And Ввожу логин и пароль
        Then Я перехожу на главную страницу 'http://localhost:4200/board'