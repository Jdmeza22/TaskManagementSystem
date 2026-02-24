⚙️ Decisiones técnicas

1. Arquitectura en capas:
 * API: controladores y middlewares (gestión de errores globales).
 * Application: servicios con la lógica de negocio (UserService, TaskService).
 * Infrastructure: acceso a datos mediante DbContext y repositorios (IUserRepository, ITaskRepository).

2. Unit of Work & Repositorios:
 * Se implementó IUnitOfWork para coordinar múltiples repositorios y asegurar consistencia en operaciones que afectan varias entidades.
   
3. Validación:
 * Se utilizó FluentValidation para validar DTOs antes de la persistencia.

4. DTOs y proyección:
  * Los controladores no devuelven directamente entidades, sino DTOs (UserResponseDto) que incluyen información calculada como TaskCount.
    Esto evita exponer datos sensibles y mejora el control sobre lo que recibe el frontend.

5. CORS configurado específicamente para Angular dev server, lo que permite la comunicación frontend-backend sin exponer la API a cualquier origen.

6. Swagger: documentación automática de todos los endpoints para facilitar pruebas durante el desarrollo.


⚠️ Funcionalidades pendientes

1. Filtros y paginación avanzados: los listados de usuarios y tareas no están paginados ni permiten filtros complejos.
2. Optimización de consultas:
 * Contar tareas actualmente requiere cargar toda la colección Tasks. Podría mejorarse con proyección directa en EF Core.
   
3. Pruebas unitarias y de integración: no hay tests implementados para servicios ni controladores.
4. Logging más robusto: se podría integrar Serilog u otra librería para registrar errores y actividad de la API en archivos o bases de datos.
5. Gestión de errores : actualmente hay un middleware global de errores, pero podrían definirse códigos de error más específicos y mensajes consistentes.
