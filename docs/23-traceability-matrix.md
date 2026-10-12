# İzlenebilirlik matrisi

Normatif kaynak FR/BR; burada hesaplanan/arayüz özelliklerinin ayrı tablo veya API implementasyonu gerektirmediği işaretlenir. API endpointleri tasarımdır, uygulama zorunluluğu değildir.

| FR | BR | Bileşen | Model | Arayüz | Test | Kabul kapısı | İstisna |
| --- | --- | --- | --- | --- | --- | --- | --- |
| FR-001 | BR-001 | CoursesService | Course | Courses | TC-001, TC-037, TC-052 | Faz 2 | API opsiyonel; MVC servis çağrısı |
| FR-002 | BR-002 | CoursesService | Course | Courses | TC-002 | Faz 2 | API opsiyonel; MVC servis çağrısı |
| FR-003 | BR-003 | CoursesService | Course | Courses | TC-003 | Faz 2 | API opsiyonel; MVC servis çağrısı |
| FR-004 | BR-004 | CoursesService | Course | Courses | TC-004, TC-049, TC-062 | Faz 2 | API opsiyonel; MVC servis çağrısı |
| FR-005 | BR-005 | CoursesService | Course | Courses | TC-005, TC-049 | Faz 2 | API opsiyonel; MVC servis çağrısı |
| FR-006 | BR-006 | ExamsService | Exam | Exams | TC-006, TC-038, TC-039 | Faz 2 | API opsiyonel; MVC servis çağrısı |
| FR-007 | BR-007 | ExamsService | Exam | Exams | TC-007, TC-048 | Faz 2 | API opsiyonel; MVC servis çağrısı |
| FR-008 | BR-008 | ExamsService | Exam | Exams | TC-008 | Faz 2 | API opsiyonel; MVC servis çağrısı |
| FR-009 | BR-009 | TopicsService | Topic | Topics | TC-009 | Faz 3 | API opsiyonel; MVC servis çağrısı |
| FR-010 | BR-010 | TopicsService | Topic | Topics | TC-010, TC-043, TC-066 | Faz 3 | API opsiyonel; MVC servis çağrısı |
| FR-011 | BR-011 | TopicsService | Topic | Topics | TC-011 | Faz 3 | API opsiyonel; MVC servis çağrısı |
| FR-012 | BR-012 | DashboardService | Hesaplanan Course/Topic/Task/Exam/Session | Dashboard | TC-012 | Faz 3 | Özet tablo yok |
| FR-013 | BR-013 | StudyTasksService | StudyTask | StudyTasks | TC-013, TC-041 | Faz 3 | API opsiyonel; MVC servis çağrısı |
| FR-014 | BR-014 | StudyTasksService | StudyTask | StudyTasks | TC-014, TC-063 | Faz 3 | API opsiyonel; MVC servis çağrısı |
| FR-015 | BR-015 | StudyTasksService | StudyTask | StudyTasks | TC-015, TC-044, TC-066 | Faz 3 | API opsiyonel; MVC servis çağrısı |
| FR-016 | BR-016 | StudyTasksService | StudyTask | StudyTasks | TC-016 | Faz 3 | API opsiyonel; MVC servis çağrısı |
| FR-017 | BR-017 | StudyPlansService | StudyPlanItem | StudyPlans | TC-017, TC-066 | Faz 3 | API opsiyonel; MVC servis çağrısı |
| FR-018 | BR-018 | StudyPlansService | StudyPlanItem | StudyPlans | TC-018 | Faz 3 | API opsiyonel; MVC servis çağrısı |
| FR-019 | BR-019 | StudyPlansService | StudyPlanItem | StudyPlans | TC-019 | Faz 3 | API opsiyonel; MVC servis çağrısı |
| FR-020 | BR-020 | StatisticsService | Hesaplanan Plan/Session/Task/Topic | Statistics | TC-020, TC-056 | Faz 4 | Özet tablo yok |
| FR-021 | BR-021 | StudySessionsService | StudySession | StudySessions | TC-021, TC-053, TC-062 | Faz 4 | API opsiyonel; MVC servis çağrısı |
| FR-022 | BR-022 | StudySessionsService | StudySession | StudySessions | TC-022 | Faz 4 | API opsiyonel; MVC servis çağrısı |
| FR-023 | BR-023 | StudySessionsService | StudySession | StudySessions | TC-023, TC-045, TC-046, TC-047, TC-054 | Faz 4 | API opsiyonel; MVC servis çağrısı |
| FR-024 | BR-024 | StudySessionsService | StudySession | StudySessions | TC-024, TC-063 | Faz 4 | API opsiyonel; MVC servis çağrısı |
| FR-025 | BR-025 | StatisticsService | Hesaplanan Plan/Session/Task/Topic | Statistics | TC-025, TC-055, TC-061 | Faz 4 | Özet tablo yok |
| FR-026 | BR-026 | DashboardService | Hesaplanan Course/Topic/Task/Exam/Session | Dashboard | TC-026, TC-065 | Faz 4 | Özet tablo yok |
| FR-027 | BR-027 | StatisticsService | Hesaplanan Plan/Session/Task/Topic | Statistics | TC-027, TC-050 | Faz 4 | Özet tablo yok |
| FR-028 | BR-028 | CoursesService | Course | Courses | TC-028 | Faz 2 | API opsiyonel; MVC servis çağrısı |
| FR-029 | BR-029 | Account adapter/infra | ApplicationUser | Identity/Profile | TC-029, TC-058 | Faz 5 | API opsiyonel; MVC servis çağrısı |
| FR-030 | BR-030 | Account adapter/infra | ApplicationUser | Identity/Profile | TC-030, TC-058, TC-061 | Faz 5 | API opsiyonel; MVC servis çağrısı |
| FR-031 | BR-031 | Security adapter/infra | ApplicationUser + tüm owner alanları | Tüm Controller | TC-031, TC-040, TC-041, TC-042, TC-052, TC-058, TC-064 | Faz 5 | API opsiyonel; MVC servis çağrısı |
| FR-032 | BR-032 | UI adapter/infra | ViewModel; DB yok | İlk6 View | TC-032, TC-060 | Faz 1 | UI: API/DB gerekmez |
| FR-033 | BR-033 | UI adapter/infra | ViewModel; DB yok | İlk6 View | TC-033, TC-043, TC-060 | Faz 1 | UI: API/DB gerekmez |
| FR-034 | BR-034 | Platform adapter/infra | SubmissionReceipt/RowVersion | Tüm write forms | TC-034, TC-042, TC-051 | Faz 2 | API opsiyonel; MVC servis çağrısı |
| FR-035 | BR-035 | Platform adapter/infra | SubmissionReceipt/RowVersion | Tüm write forms | TC-035, TC-057 | Faz 2 | API opsiyonel; MVC servis çağrısı |
| FR-036 | BR-036 | Security adapter/infra | ApplicationUser + tüm owner alanları | Tüm Controller | TC-036, TC-059, TC-064 | Faz 2 | API opsiyonel; MVC servis çağrısı |

[İndeks](../README.md)
