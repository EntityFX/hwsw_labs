# АСВТ (Занятие 6). Сетевые приложения. HTTP сервер-клиент.

## Цель

Разработка клиент-серверных приложений на языке программирования C# с использованием протокола HTTP.

Пример приложений:

* HTTP AspNet Core Веб серыер - Создаёт HTTP сервер и решает задачи:
    * Создание пользователя
    * Удаление пользователя
    * Редактирование пользователя
    * Получение пользователя по уникальному ключу
    * Получение списка пользователей
* HTTP клиент - подключается к серверу, отправляет команды, управляет пользователями и выводит результаты

## Задача

Модифицировать приложения HTTP клиент-сервера, чтобы приложения
HTTP клиент-сервера управляли не пользователями, а чем-нибудь другим (например кассой, погодой или ещё, придумать своё). Приложения UDP клиент-сервера должны отправлять не текстовые сообщения, а какой-нибудь другой вид сообщения (Своя реализация сообщения JSON).
В качестве хранилища использовать формат JSON или XML или YAML.

HTTP приложение (клиент и сервер):

```
+------------+    HTTP запрос                          +------------+
| HTTP-клиент| --------------------------------------> | HTTP-сервер|
+------------+ <-------------------------------------  +------------+
                  ответ 
```

## Структура кода