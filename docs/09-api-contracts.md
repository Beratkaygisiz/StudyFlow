# REST API tasarım sözleşmeleri

Durum: tasarım; bu depoda çalışan uygulama veya yürütülmüş uygulama testleri yoktur.

## Durum ve ortak sözleşme
Bu dosya nihai sistem için tasarımdır; endpoint’ler uygulanmış değildir. MVP MVC ile tamamlanabilir. API yapılmazsa aşağıdaki command/query DTO’ları MVC Application servislerine eşlenir; HTTP testleri N/A olur. API geliştirme opsiyonel Faz 6 sonrası issue’sudur.

Base `/api`; aynı origin Identity cookie; her endpoint giriş gerektirir. Kimlik claim’den; UserId alanı request’te reddedilir. Cookie kullanan tüm POST/PUT/DELETE istekleri anti-forgery header ister; eksik/geçersiz token 400. Ayrı mobil istemci/JWT kapsam dışı. Girişsiz API 401, izin yoksa 403, başka sahibin kaynak/ilişkisi 404. MVC login redirect davranışı API’ye taşınmaz.

JSON camelCase; enum string (tabloda listelenen değerler), DateOnly YYYY-MM-DD, zaman ISO8601 offset zorunlu ve UTC’ye normalize; saniye hassasiyeti. Bilinmeyen JSON alanları 400 ile reddedilir. `{id}` pozitif int; malformed 400, olmayan 404. Liste page=1, pageSize=20, üst sınır100; kararlı Id tie-break; tarih aralığı iki uç dahil, en fazla366 gün. Liste response `{items:[],page:1,pageSize:20,totalCount:0}`. Filters tabloda yoksa desteklenmez.

GET detail ETag: `"<base64 rowversion>"`; PUT/DELETE If-Match zorunlu: yok428, yanlış biçim400, eski409. DTO `version` response’ta base64, body’de client yazamaz. Create Idempotency-Key UUID header zorunlu; MVC’de OperationId hidden; yok400. Aynı payload/key ilk sonucu yeniden verir; kaynak silinmişse409. POST create201+Location, replay200; PUT200+güncel DTO, DELETE204. Idempotent bitirme replay200 aynı DTO. GET200. Hata gövdeleri bütün endpoint’lerde aşağıdaki formatta. Request body olmayan GET/DELETE JSON göndermez; action POST stop `{}` alır.

```json
{"type":"urn:studyflow:error:validation","title":"Geçersiz veri","status":400,"detail":"Alanları kontrol edin.","instance":"/api/courses","traceId":"sample-trace","errors":{"name":["Ders adı zorunludur."]}}
```

NotFound404, Conflict409 (archive/duplicate/overlap/concurrency), Unauthorized401, Forbidden403; beklenmedik500 genel mesaj/traceId. Tüm endpoint’lerde ortak kodlar400/401/403/500; ilgili resource endpoint’lerinde404; write409 ve PUT/DELETE428. List/detail validation domain ile aynıdır.

## DTO kataloğu
Response kaynak DTO’su: domain alanlarının camelCase karşılığı; UserId çıkarılır; RowVersion yerine version; Id/CreatedAt read-only. DurationMinutes ve remainingDays yalnız hesaplanan response alanıdır. Navigation entity’leri döndürülmez. SubmissionReceipt API kaynağı değildir. Standart update PUT tüm yazılabilir alanları ister; opsiyonel nullable alanlar açıkça null gönderilir; kısmi PATCH yok.

| Metot | URL | Amaç | Gereksinimler |
| --- | --- | --- | --- |
| GET | /api/courses | Listele ve filtrele | FR-001, FR-002, FR-003, FR-004, FR-005, FR-028 |
| GET | /api/courses/{id} | Detay | FR-001, FR-002, FR-003, FR-004, FR-005, FR-028 |
| POST | /api/courses | Oluştur | FR-001, FR-002, FR-003, FR-004, FR-005, FR-028 |
| PUT | /api/courses/{id} | Tam güncelle | FR-001, FR-002, FR-003, FR-004, FR-005, FR-028 |
| DELETE | /api/courses/{id} | Sahip olunan kaydı sil | FR-001, FR-002, FR-003, FR-004, FR-005, FR-028 |
| GET | /api/exams | Listele ve filtrele | FR-006, FR-007, FR-008 |
| GET | /api/exams/{id} | Detay | FR-006, FR-007, FR-008 |
| POST | /api/exams | Oluştur | FR-006, FR-007, FR-008 |
| PUT | /api/exams/{id} | Tam güncelle | FR-006, FR-007, FR-008 |
| DELETE | /api/exams/{id} | Sahip olunan kaydı sil | FR-006, FR-007, FR-008 |
| GET | /api/topics | Listele ve filtrele | FR-009, FR-010, FR-011, FR-012 |
| GET | /api/topics/{id} | Detay | FR-009, FR-010, FR-011, FR-012 |
| POST | /api/topics | Oluştur | FR-009, FR-010, FR-011, FR-012 |
| PUT | /api/topics/{id} | Tam güncelle | FR-009, FR-010, FR-011, FR-012 |
| DELETE | /api/topics/{id} | Sahip olunan kaydı sil | FR-009, FR-010, FR-011, FR-012 |
| GET | /api/study-tasks | Listele ve filtrele | FR-013, FR-014, FR-015, FR-016 |
| GET | /api/study-tasks/{id} | Detay | FR-013, FR-014, FR-015, FR-016 |
| POST | /api/study-tasks | Oluştur | FR-013, FR-014, FR-015, FR-016 |
| PUT | /api/study-tasks/{id} | Tam güncelle | FR-013, FR-014, FR-015, FR-016 |
| DELETE | /api/study-tasks/{id} | Sahip olunan kaydı sil | FR-013, FR-014, FR-015, FR-016 |
| GET | /api/study-plans | Listele ve filtrele | FR-017, FR-018, FR-019, FR-020 |
| GET | /api/study-plans/{id} | Detay | FR-017, FR-018, FR-019, FR-020 |
| POST | /api/study-plans | Oluştur | FR-017, FR-018, FR-019, FR-020 |
| PUT | /api/study-plans/{id} | Tam güncelle | FR-017, FR-018, FR-019, FR-020 |
| DELETE | /api/study-plans/{id} | Sahip olunan kaydı sil | FR-017, FR-018, FR-019, FR-020 |
| GET | /api/study-sessions | Listele ve filtrele | FR-021, FR-022, FR-023, FR-024, FR-025 |
| GET | /api/study-sessions/{id} | Detay | FR-021, FR-022, FR-023, FR-024, FR-025 |
| POST | /api/study-sessions | Oluştur | FR-021, FR-022, FR-023, FR-024, FR-025 |
| PUT | /api/study-sessions/{id} | Tam güncelle | FR-021, FR-022, FR-023, FR-024, FR-025 |
| DELETE | /api/study-sessions/{id} | Sahip olunan kaydı sil | FR-021, FR-022, FR-023, FR-024, FR-025 |
| POST | /api/study-sessions/start | Zamanlayıcı başlat | FR-021 |
| POST | /api/study-sessions/{id}/stop | Açık oturumu bitir | FR-022 |
| GET | /api/dashboard | Öğrenci özeti | FR-026, FR-028 |
| GET | /api/statistics | Aralık analizi | FR-020, FR-025, FR-027 |

### GET /api/courses

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Listele ve filtrele / Course |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | search, status=Active/Archived, page, pageSize |
| Request | Body yok |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,500 |
| Doğrulama | name 1..120; weeklyTargetMinutes 0..10080; create isArchived=false; update archive FR-004 |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-001, FR-002, FR-003, FR-004, FR-005, FR-028 |

Response örneği:

```json
{
  "items": [
    {
      "id": 1,
      "name": "Algoritmalar",
      "weeklyTargetMinutes": 120,
      "isArchived": false,
      "createdAt": "2026-11-01T10:00:00Z",
      "version": "AAAAAAAAB9E="
    }
  ],
  "page": 1,
  "pageSize": 20,
  "totalCount": 1
}
```

### GET /api/courses/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Detay / Course |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id |
| Request | Body yok |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200+ETag; 400,401,403,404,500 |
| Doğrulama | name 1..120; weeklyTargetMinutes 0..10080; create isArchived=false; update archive FR-004 |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-001, FR-002, FR-003, FR-004, FR-005, FR-028 |

Response örneği:

```json
{
  "id": 1,
  "name": "Algoritmalar",
  "weeklyTargetMinutes": 120,
  "isArchived": false,
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E="
}
```

### POST /api/courses

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Oluştur / Course |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | Idempotency-Key |
| Request | Aşağıdaki tam JSON; alan tipleri DTO/domain kataloğunda |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 201+Location veya replay200; 400,401,403,404,409,500 |
| Doğrulama | name 1..120; weeklyTargetMinutes 0..10080; create isArchived=false; update archive FR-004 |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-001, FR-002, FR-003, FR-004, FR-005, FR-028 |

Request örneği:

```json
{
  "name": "Algoritmalar",
  "weeklyTargetMinutes": 120,
  "isArchived": false
}
```

Response örneği:

```json
{
  "id": 1,
  "name": "Algoritmalar",
  "weeklyTargetMinutes": 120,
  "isArchived": false,
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E="
}
```

### PUT /api/courses/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Tam güncelle / Course |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id; If-Match |
| Request | Aşağıdaki tam JSON; alan tipleri DTO/domain kataloğunda |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,404,409,428,500 |
| Doğrulama | name 1..120; weeklyTargetMinutes 0..10080; create isArchived=false; update archive FR-004 |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-001, FR-002, FR-003, FR-004, FR-005, FR-028 |

Request örneği:

```json
{
  "name": "Algoritmalar",
  "weeklyTargetMinutes": 120,
  "isArchived": false
}
```

Response örneği:

```json
{
  "id": 1,
  "name": "Algoritmalar",
  "weeklyTargetMinutes": 120,
  "isArchived": false,
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E="
}
```

### DELETE /api/courses/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Sahip olunan kaydı sil / Course |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id; If-Match |
| Request | Body yok |
| Response | Gövde yok (204) |
| Durumlar | 204; 400,401,403,404,409,428,500 |
| Doğrulama | name 1..120; weeklyTargetMinutes 0..10080; create isArchived=false; update archive FR-004 |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-001, FR-002, FR-003, FR-004, FR-005, FR-028 |

### GET /api/exams

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Listele ve filtrele / Exam |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | courseId, scope=Upcoming/Past/All, page, pageSize |
| Request | Body yok |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,500 |
| Doğrulama | type Midterm/Final/Quiz/Other; title1..120; parse edilmiş tarih; CourseId etkin/sahip |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-006, FR-007, FR-008 |

Response örneği:

```json
{
  "items": [
    {
      "id": 1,
      "courseId": 1,
      "title": "Vize",
      "type": "Midterm",
      "scheduledAt": "2026-11-10T10:00:00+03:00",
      "createdAt": "2026-11-01T10:00:00Z",
      "version": "AAAAAAAAB9E="
    }
  ],
  "page": 1,
  "pageSize": 20,
  "totalCount": 1
}
```

### GET /api/exams/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Detay / Exam |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id |
| Request | Body yok |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200+ETag; 400,401,403,404,500 |
| Doğrulama | type Midterm/Final/Quiz/Other; title1..120; parse edilmiş tarih; CourseId etkin/sahip |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-006, FR-007, FR-008 |

Response örneği:

```json
{
  "id": 1,
  "courseId": 1,
  "title": "Vize",
  "type": "Midterm",
  "scheduledAt": "2026-11-10T10:00:00+03:00",
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E="
}
```

### POST /api/exams

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Oluştur / Exam |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | Idempotency-Key |
| Request | Aşağıdaki tam JSON; alan tipleri DTO/domain kataloğunda |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 201+Location veya replay200; 400,401,403,404,409,500 |
| Doğrulama | type Midterm/Final/Quiz/Other; title1..120; parse edilmiş tarih; CourseId etkin/sahip |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-006, FR-007, FR-008 |

Request örneği:

```json
{
  "courseId": 1,
  "title": "Vize",
  "type": "Midterm",
  "scheduledAt": "2026-11-10T10:00:00+03:00"
}
```

Response örneği:

```json
{
  "id": 1,
  "courseId": 1,
  "title": "Vize",
  "type": "Midterm",
  "scheduledAt": "2026-11-10T10:00:00+03:00",
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E="
}
```

### PUT /api/exams/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Tam güncelle / Exam |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id; If-Match |
| Request | Aşağıdaki tam JSON; alan tipleri DTO/domain kataloğunda |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,404,409,428,500 |
| Doğrulama | type Midterm/Final/Quiz/Other; title1..120; parse edilmiş tarih; CourseId etkin/sahip |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-006, FR-007, FR-008 |

Request örneği:

```json
{
  "courseId": 1,
  "title": "Vize",
  "type": "Midterm",
  "scheduledAt": "2026-11-10T10:00:00+03:00"
}
```

Response örneği:

```json
{
  "id": 1,
  "courseId": 1,
  "title": "Vize",
  "type": "Midterm",
  "scheduledAt": "2026-11-10T10:00:00+03:00",
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E="
}
```

### DELETE /api/exams/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Sahip olunan kaydı sil / Exam |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id; If-Match |
| Request | Body yok |
| Response | Gövde yok (204) |
| Durumlar | 204; 400,401,403,404,409,428,500 |
| Doğrulama | type Midterm/Final/Quiz/Other; title1..120; parse edilmiş tarih; CourseId etkin/sahip |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-006, FR-007, FR-008 |

### GET /api/topics

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Listele ve filtrele / Topic |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | courseId, status=NotStarted/InProgress/Completed, page, pageSize |
| Request | Body yok |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,500 |
| Doğrulama | title1..160; notes<=2000; sıra>=0; konu bağlıysa courseId değiştirilemez |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-009, FR-010, FR-011, FR-012 |

Response örneği:

```json
{
  "items": [
    {
      "id": 1,
      "courseId": 1,
      "title": "Diziler",
      "status": "NotStarted",
      "sortOrder": 10,
      "notes": null,
      "reviewOn": null,
      "createdAt": "2026-11-01T10:00:00Z",
      "version": "AAAAAAAAB9E="
    }
  ],
  "page": 1,
  "pageSize": 20,
  "totalCount": 1
}
```

### GET /api/topics/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Detay / Topic |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id |
| Request | Body yok |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200+ETag; 400,401,403,404,500 |
| Doğrulama | title1..160; notes<=2000; sıra>=0; konu bağlıysa courseId değiştirilemez |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-009, FR-010, FR-011, FR-012 |

Response örneği:

```json
{
  "id": 1,
  "courseId": 1,
  "title": "Diziler",
  "status": "NotStarted",
  "sortOrder": 10,
  "notes": null,
  "reviewOn": null,
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E="
}
```

### POST /api/topics

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Oluştur / Topic |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | Idempotency-Key |
| Request | Aşağıdaki tam JSON; alan tipleri DTO/domain kataloğunda |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 201+Location veya replay200; 400,401,403,404,409,500 |
| Doğrulama | title1..160; notes<=2000; sıra>=0; konu bağlıysa courseId değiştirilemez |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-009, FR-010, FR-011, FR-012 |

Request örneği:

```json
{
  "courseId": 1,
  "title": "Diziler",
  "status": "NotStarted",
  "sortOrder": 10,
  "notes": null,
  "reviewOn": null
}
```

Response örneği:

```json
{
  "id": 1,
  "courseId": 1,
  "title": "Diziler",
  "status": "NotStarted",
  "sortOrder": 10,
  "notes": null,
  "reviewOn": null,
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E="
}
```

### PUT /api/topics/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Tam güncelle / Topic |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id; If-Match |
| Request | Aşağıdaki tam JSON; alan tipleri DTO/domain kataloğunda |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,404,409,428,500 |
| Doğrulama | title1..160; notes<=2000; sıra>=0; konu bağlıysa courseId değiştirilemez |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-009, FR-010, FR-011, FR-012 |

Request örneği:

```json
{
  "courseId": 1,
  "title": "Diziler",
  "status": "NotStarted",
  "sortOrder": 10,
  "notes": null,
  "reviewOn": null
}
```

Response örneği:

```json
{
  "id": 1,
  "courseId": 1,
  "title": "Diziler",
  "status": "NotStarted",
  "sortOrder": 10,
  "notes": null,
  "reviewOn": null,
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E="
}
```

### DELETE /api/topics/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Sahip olunan kaydı sil / Topic |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id; If-Match |
| Request | Body yok |
| Response | Gövde yok (204) |
| Durumlar | 204; 400,401,403,404,409,428,500 |
| Doğrulama | title1..160; notes<=2000; sıra>=0; konu bağlıysa courseId değiştirilemez |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-009, FR-010, FR-011, FR-012 |

### GET /api/study-tasks

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Listele ve filtrele / StudyTask |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | courseId, status, priority, from, to, overdue=true/false, page, pageSize |
| Request | Body yok |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,500 |
| Doğrulama | title1..160; tahmin1..1440; enum Pending/InProgress/Completed ve Low/Normal/High; konu aynı ders; CompletedAt sunucu |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-013, FR-014, FR-015, FR-016 |

Response örneği:

```json
{
  "items": [
    {
      "id": 1,
      "courseId": 1,
      "topicId": 1,
      "title": "20 soru çöz",
      "status": "Pending",
      "priority": "High",
      "dueOn": "2026-11-12",
      "estimatedMinutes": 60,
      "createdAt": "2026-11-01T10:00:00Z",
      "version": "AAAAAAAAB9E=",
      "completedAt": null
    }
  ],
  "page": 1,
  "pageSize": 20,
  "totalCount": 1
}
```

### GET /api/study-tasks/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Detay / StudyTask |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id |
| Request | Body yok |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200+ETag; 400,401,403,404,500 |
| Doğrulama | title1..160; tahmin1..1440; enum Pending/InProgress/Completed ve Low/Normal/High; konu aynı ders; CompletedAt sunucu |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-013, FR-014, FR-015, FR-016 |

Response örneği:

```json
{
  "id": 1,
  "courseId": 1,
  "topicId": 1,
  "title": "20 soru çöz",
  "status": "Pending",
  "priority": "High",
  "dueOn": "2026-11-12",
  "estimatedMinutes": 60,
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E=",
  "completedAt": null
}
```

### POST /api/study-tasks

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Oluştur / StudyTask |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | Idempotency-Key |
| Request | Aşağıdaki tam JSON; alan tipleri DTO/domain kataloğunda |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 201+Location veya replay200; 400,401,403,404,409,500 |
| Doğrulama | title1..160; tahmin1..1440; enum Pending/InProgress/Completed ve Low/Normal/High; konu aynı ders; CompletedAt sunucu |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-013, FR-014, FR-015, FR-016 |

Request örneği:

```json
{
  "courseId": 1,
  "topicId": 1,
  "title": "20 soru çöz",
  "status": "Pending",
  "priority": "High",
  "dueOn": "2026-11-12",
  "estimatedMinutes": 60
}
```

Response örneği:

```json
{
  "id": 1,
  "courseId": 1,
  "topicId": 1,
  "title": "20 soru çöz",
  "status": "Pending",
  "priority": "High",
  "dueOn": "2026-11-12",
  "estimatedMinutes": 60,
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E=",
  "completedAt": null
}
```

### PUT /api/study-tasks/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Tam güncelle / StudyTask |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id; If-Match |
| Request | Aşağıdaki tam JSON; alan tipleri DTO/domain kataloğunda |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,404,409,428,500 |
| Doğrulama | title1..160; tahmin1..1440; enum Pending/InProgress/Completed ve Low/Normal/High; konu aynı ders; CompletedAt sunucu |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-013, FR-014, FR-015, FR-016 |

Request örneği:

```json
{
  "courseId": 1,
  "topicId": 1,
  "title": "20 soru çöz",
  "status": "Pending",
  "priority": "High",
  "dueOn": "2026-11-12",
  "estimatedMinutes": 60
}
```

Response örneği:

```json
{
  "id": 1,
  "courseId": 1,
  "topicId": 1,
  "title": "20 soru çöz",
  "status": "Pending",
  "priority": "High",
  "dueOn": "2026-11-12",
  "estimatedMinutes": 60,
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E=",
  "completedAt": null
}
```

### DELETE /api/study-tasks/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Sahip olunan kaydı sil / StudyTask |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id; If-Match |
| Request | Body yok |
| Response | Gövde yok (204) |
| Durumlar | 204; 400,401,403,404,409,428,500 |
| Doğrulama | title1..160; tahmin1..1440; enum Pending/InProgress/Completed ve Low/Normal/High; konu aynı ders; CompletedAt sunucu |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-013, FR-014, FR-015, FR-016 |

### GET /api/study-plans

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Listele ve filtrele / StudyPlanItem |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | from, to, courseId, page, pageSize |
| Request | Body yok |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,500 |
| Doğrulama | süre1..1440; task sahibi/aktif; aynı TaskId/gün unique; courseId görevden sunucu üretir |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-017, FR-018, FR-019, FR-020 |

Response örneği:

```json
{
  "items": [
    {
      "id": 1,
      "taskId": 1,
      "plannedOn": "2026-11-12",
      "plannedMinutes": 60,
      "createdAt": "2026-11-01T10:00:00Z",
      "version": "AAAAAAAAB9E=",
      "courseId": 1
    }
  ],
  "page": 1,
  "pageSize": 20,
  "totalCount": 1
}
```

### GET /api/study-plans/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Detay / StudyPlanItem |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id |
| Request | Body yok |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200+ETag; 400,401,403,404,500 |
| Doğrulama | süre1..1440; task sahibi/aktif; aynı TaskId/gün unique; courseId görevden sunucu üretir |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-017, FR-018, FR-019, FR-020 |

Response örneği:

```json
{
  "id": 1,
  "taskId": 1,
  "plannedOn": "2026-11-12",
  "plannedMinutes": 60,
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E=",
  "courseId": 1
}
```

### POST /api/study-plans

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Oluştur / StudyPlanItem |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | Idempotency-Key |
| Request | Aşağıdaki tam JSON; alan tipleri DTO/domain kataloğunda |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 201+Location veya replay200; 400,401,403,404,409,500 |
| Doğrulama | süre1..1440; task sahibi/aktif; aynı TaskId/gün unique; courseId görevden sunucu üretir |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-017, FR-018, FR-019, FR-020 |

Request örneği:

```json
{
  "taskId": 1,
  "plannedOn": "2026-11-12",
  "plannedMinutes": 60
}
```

Response örneği:

```json
{
  "id": 1,
  "taskId": 1,
  "plannedOn": "2026-11-12",
  "plannedMinutes": 60,
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E=",
  "courseId": 1
}
```

### PUT /api/study-plans/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Tam güncelle / StudyPlanItem |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id; If-Match |
| Request | Aşağıdaki tam JSON; alan tipleri DTO/domain kataloğunda |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,404,409,428,500 |
| Doğrulama | süre1..1440; task sahibi/aktif; aynı TaskId/gün unique; courseId görevden sunucu üretir |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-017, FR-018, FR-019, FR-020 |

Request örneği:

```json
{
  "taskId": 1,
  "plannedOn": "2026-11-12",
  "plannedMinutes": 60
}
```

Response örneği:

```json
{
  "id": 1,
  "taskId": 1,
  "plannedOn": "2026-11-12",
  "plannedMinutes": 60,
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E=",
  "courseId": 1
}
```

### DELETE /api/study-plans/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Sahip olunan kaydı sil / StudyPlanItem |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id; If-Match |
| Request | Body yok |
| Response | Gövde yok (204) |
| Durumlar | 204; 400,401,403,404,409,428,500 |
| Doğrulama | süre1..1440; task sahibi/aktif; aynı TaskId/gün unique; courseId görevden sunucu üretir |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-017, FR-018, FR-019, FR-020 |

### GET /api/study-sessions

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Listele ve filtrele / StudySession |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | from, to, courseId, taskId, open=true/false, page, pageSize |
| Request | Body yok |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,500 |
| Doğrulama | manuel POST/PUT bitmiş kayıt; start<end<=now; <=24saat; çakışma yok; açık kayıt PUT kabul etmez |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-021, FR-022, FR-023, FR-024, FR-025 |

Response örneği:

```json
{
  "items": [
    {
      "id": 1,
      "courseId": 1,
      "taskId": 1,
      "topicId": 1,
      "startAt": "2026-11-12T09:00:00+03:00",
      "endAt": "2026-11-12T10:00:00+03:00",
      "createdAt": "2026-11-01T10:00:00Z",
      "version": "AAAAAAAAB9E=",
      "source": "Manual",
      "durationMinutes": 60
    }
  ],
  "page": 1,
  "pageSize": 20,
  "totalCount": 1
}
```

### GET /api/study-sessions/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Detay / StudySession |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id |
| Request | Body yok |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200+ETag; 400,401,403,404,500 |
| Doğrulama | manuel POST/PUT bitmiş kayıt; start<end<=now; <=24saat; çakışma yok; açık kayıt PUT kabul etmez |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-021, FR-022, FR-023, FR-024, FR-025 |

Response örneği:

```json
{
  "id": 1,
  "courseId": 1,
  "taskId": 1,
  "topicId": 1,
  "startAt": "2026-11-12T09:00:00+03:00",
  "endAt": "2026-11-12T10:00:00+03:00",
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E=",
  "source": "Manual",
  "durationMinutes": 60
}
```

### POST /api/study-sessions

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Oluştur / StudySession |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | Idempotency-Key |
| Request | Aşağıdaki tam JSON; alan tipleri DTO/domain kataloğunda |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 201+Location veya replay200; 400,401,403,404,409,500 |
| Doğrulama | manuel POST/PUT bitmiş kayıt; start<end<=now; <=24saat; çakışma yok; açık kayıt PUT kabul etmez |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-021, FR-022, FR-023, FR-024, FR-025 |

Request örneği:

```json
{
  "courseId": 1,
  "taskId": 1,
  "topicId": 1,
  "startAt": "2026-11-12T09:00:00+03:00",
  "endAt": "2026-11-12T10:00:00+03:00"
}
```

Response örneği:

```json
{
  "id": 1,
  "courseId": 1,
  "taskId": 1,
  "topicId": 1,
  "startAt": "2026-11-12T09:00:00+03:00",
  "endAt": "2026-11-12T10:00:00+03:00",
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E=",
  "source": "Manual",
  "durationMinutes": 60
}
```

### PUT /api/study-sessions/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Tam güncelle / StudySession |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id; If-Match |
| Request | Aşağıdaki tam JSON; alan tipleri DTO/domain kataloğunda |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,404,409,428,500 |
| Doğrulama | manuel POST/PUT bitmiş kayıt; start<end<=now; <=24saat; çakışma yok; açık kayıt PUT kabul etmez |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-021, FR-022, FR-023, FR-024, FR-025 |

Request örneği:

```json
{
  "courseId": 1,
  "taskId": 1,
  "topicId": 1,
  "startAt": "2026-11-12T09:00:00+03:00",
  "endAt": "2026-11-12T10:00:00+03:00"
}
```

Response örneği:

```json
{
  "id": 1,
  "courseId": 1,
  "taskId": 1,
  "topicId": 1,
  "startAt": "2026-11-12T09:00:00+03:00",
  "endAt": "2026-11-12T10:00:00+03:00",
  "createdAt": "2026-11-01T10:00:00Z",
  "version": "AAAAAAAAB9E=",
  "source": "Manual",
  "durationMinutes": 60
}
```

### DELETE /api/study-sessions/{id}

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Sahip olunan kaydı sil / StudySession |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id; If-Match |
| Request | Body yok |
| Response | Gövde yok (204) |
| Durumlar | 204; 400,401,403,404,409,428,500 |
| Doğrulama | manuel POST/PUT bitmiş kayıt; start<end<=now; <=24saat; çakışma yok; açık kayıt PUT kabul etmez |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-021, FR-022, FR-023, FR-024, FR-025 |

### POST /api/study-sessions/start

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Zamanlayıcı başlat / StudySession |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | Idempotency-Key |
| Request | Aşağıdaki tam JSON; alan tipleri DTO/domain kataloğunda |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 201 veya replay200; 400,401,403,404,409,500 |
| Doğrulama | Sunucu now; tek açık; aynı ders ilişkileri; owner transaction |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-021 |

Request örneği:

```json
{
  "courseId": 1,
  "taskId": null,
  "topicId": null
}
```

Response örneği:

```json
{
  "id": 2,
  "courseId": 1,
  "taskId": null,
  "topicId": null,
  "startAt": "2026-11-12T10:00:00Z",
  "endAt": null,
  "source": "Timer",
  "createdAt": "2026-11-12T10:00:00Z",
  "version": "AAAAAAAAB9I="
}
```

### POST /api/study-sessions/{id}/stop

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Açık oturumu bitir / StudySession |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | id; If-Match |
| Request | Aşağıdaki tam JSON; alan tipleri DTO/domain kataloğunda |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,404,409,428,500 |
| Doğrulama | EndAt sunucu; bitmiş replay eski token ile aynı bitişi döndürür; açık kayıtta concurrency zorunlu |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-022 |

Request örneği:

```json
{}
```

Response örneği:

```json
{
  "id": 2,
  "courseId": 1,
  "taskId": null,
  "topicId": null,
  "startAt": "2026-11-12T10:00:00Z",
  "endAt": "2026-11-12T11:00:00Z",
  "source": "Timer",
  "durationMinutes": 60,
  "createdAt": "2026-11-12T10:00:00Z",
  "version": "AAAAAAAAB9M="
}
```

### GET /api/dashboard

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Öğrenci özeti / DashboardSummary |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | yok |
| Request | Body yok |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,500 |
| Doğrulama | Mevcut kullanıcının bu haftası; Pazartesi sırası; N=5 tamamlanmış, 14 gün sınav |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-026, FR-028 |

Response örneği:

```json
{
  "activeCourseCount": 2,
  "pendingTaskCount": 3,
  "overdueTaskCount": 1,
  "weeklyTargetMinutes": 300,
  "weeklyActualMinutes": 120,
  "upcomingExams": [],
  "recentCompletedTasks": [],
  "courseProgress": [],
  "dailyMinutes": [
    0,
    0,
    0,
    0,
    0,
    0,
    120
  ]
}
```

### GET /api/statistics

| Alan | Sözleşme |
| --- | --- |
| Amaç / modül | Aralık analizi / StatisticsSummary |
| Yetki | Authenticated; owner kapsamı; writes anti-forgery |
| Path/query/header | from,to zorunlu |
| Request | Body yok |
| Response | Aşağıdaki JSON şekli; örnek tasarım, gerçek yanıt değil |
| Durumlar | 200; 400,401,403,500 |
| Doğrulama | En fazla366 gün; önceki aynı gün sayısı; oranlar FR-027; boş aralık sıfır |
| Hata cevabı | Ortak ProblemDetails; alan hatası errors; güvenli detail |
| İlişki | FR-020, FR-025, FR-027 |

Response örneği:

```json
{
  "from": "2026-11-09",
  "to": "2026-11-15",
  "actualMinutes": 0,
  "plannedMinutes": 0,
  "taskCompletionPercent": 0,
  "overdueTaskCount": 0,
  "courseDistribution": [],
  "topicProgress": [],
  "dailyMinutes": [],
  "planComparisons": [],
  "unplannedMinutes": 0,
  "previousPeriodChangePercent": null
}
```

## Summary alanları ve filtre ayrıntısı
Dashboard courseProgress: `{courseId,courseName,completedTopicCount,totalTopicCount,completionPercent}`; recentCompletedTasks: `{id,title,completedAt}`; upcomingExams: `{id,courseId,title,type,scheduledAt,remainingDays}`. Statistics dailyMinutes: `{date,actualMinutes,plannedMinutes}`; courseDistribution `{courseId,courseName,actualMinutes}`; topicProgress aynı courseProgress; planComparisons `{planItemId,taskId,date,plannedMinutes,actualMinutes,differenceMinutes}`. unplannedMinutes = toplam süre - plan kaydı bulunan görev/günlere dağıtılmış gerçek süre; negatif olmaz. actualMinutes hiçbir zaman planla sınırlandırılmaz. Aralık filtresi oturumlarla kesişim üzerinden kırpılır, yalnız StartAt aralık içinde filtresi değildir. Session detail/list açık kayıtta durationMinutes=null.

Course archive için PUT courses/{id} tam body isArchived; ayrı RPC archive endpoint yok. Task durum için PUT tam DTO; tekrar Completed BR-015. Exam/Topic course değişimi servis ilişki kısıtlarıyla; Topic bağlı değilse yeni aktif derse taşınabilir. Task TopicId bağlı oturum varken değiştirilemez. Account UI Identity MVC'dir; hesap REST endpoint’i bu kapsamda yoktur.

[Dokümantasyon indeksi](../README.md)
