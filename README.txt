Add-Migration InitialCreate -Context IdentityDBContext -StartupProject IdentityMService -Project IdentityMService
Update-Database -Context IdentityDBContext -StartupProject IdentityMService -Project IdentityMService

EntityFrameworkCore\Add-Migration InitialCreate111 -StartupProject IdentityMService -Project IdentityMService
EntityFrameworkCore\Update-Database -StartupProject IdentityMService -Project IdentityMService




Архитектура микросервисов
В системе можно выделить 7 основных микросервисов, каждый из которых отвечает за свою область.

1️⃣ Frontend (Blazor WebAssembly)
📌 Задачи:

UI для пользователей (поиск отелей, бронирование, просмотр истории заказов).
UI для администраторов (управление отелями, номерами, бронями).
Взаимодействие через API Gateway.
2️⃣ API Gateway (Ocelot/YARP)
📌 Задачи:

Единая точка входа для всех запросов.
Маршрутизация к нужным микросервисам.
Авторизация и аутентификация.
3️⃣ User Service (Сервис пользователей)
📌 Задачи:

Регистрация, авторизация (JWT, IdentityServer4).
Хранение данных о пользователях.
Роли (гости, администраторы, владельцы отелей).
Связь с Booking Service (пользователь видит свои брони).
💾 Хранилище: SQL Server / PostgreSQL.

4️⃣ Hotel Service (Сервис отелей)
📌 Задачи:

Управление списком отелей и номеров.
CRUD-операции: добавление, удаление, изменение отелей и номеров.
Поиск отелей по параметрам (фильтры, локация, цена).
Связь с Booking Service (проверка доступности номеров).
💾 Хранилище: SQL Server / MongoDB.

5️⃣ Booking Service (Сервис бронирования)
📌 Задачи:

Обработка бронирования номеров.
Проверка доступности номера перед бронированием.
Управление статусами брони (ожидание оплаты, подтверждено, отменено).
Отправка событий в Payment Service (если бронь требует оплаты).
💾 Хранилище: SQL Server / Redis (для кэша доступных номеров).

6️⃣ Payment Service (Сервис платежей)
📌 Задачи:

Проведение платежей (Stripe, PayPal, банковские карты).
Связь с Booking Service (если оплата успешна → подтверждение брони).
Ведение истории платежей.
💾 Хранилище: SQL Server.

7️⃣ Notification Service (Сервис уведомлений)
📌 Задачи:

Отправка уведомлений по Email / SMS / WebSocket.
Информирование пользователей об изменениях бронирования.
Интеграция с Booking Service (например, уведомление о подтверждении брони).
💾 Хранилище: MongoDB (хранение логов уведомлений).

Как общаются микросервисы?
🔹 API Gateway – единая точка входа для клиентов.
🔹 HTTP-запросы (REST) – для обычных операций (поиск отелей, бронирование).
🔹 Message Broker (RabbitMQ / Kafka) – для событий (успешная оплата → подтверждение брони).

Пример потока работы:

Пользователь ищет отель в Blazor → запрос идет в API Gateway.
API Gateway перенаправляет в Hotel Service, который выдает список отелей.
Пользователь выбирает номер → отправляет запрос в Booking Service.
Booking Service проверяет доступность номера в Hotel Service.
Если бронь требует оплаты, Booking Service отправляет событие в Payment Service.
Payment Service обрабатывает платеж и отправляет подтверждение обратно.
Booking Service обновляет бронь и передает в Notification Service для отправки уведомления.

