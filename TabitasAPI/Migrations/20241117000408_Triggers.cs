using Microsoft.EntityFrameworkCore.Migrations;
using TabitasAPI.DTOs;

#nullable disable

namespace TabitasAPI.Migrations
{
    /// <inheritdoc />
    public partial class Triggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Procedimiento:proceso
            migrationBuilder.Sql(@"
            CREATE PROCEDURE UpdateGeneralesProceso
            @IdGeneral INT
            AS
            BEGIN
            UPDATE Generales
            SET IdProceso = '2'
            WHERE IdGeneral = @IdGeneral;
            END;
        ");
            // Trigger:proceso
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trigger_actualizar_proceso_actual]
            ON [dbo].[Almacenes]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdGeneral INT;
            SELECT TOP 1 @IdGeneral = IdGeneral FROM inserted;
            EXEC UpdateGeneralesProceso @IdGeneral;
            END;
            GO
        ");
            // Trigger: insertar 0
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trigger_invoke_set_fecha_editada_on_insert]
            ON [dbo].[Almacenes]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdAlmacen INT;
            SELECT @IdAlmacen = IdAlmacen FROM inserted;
            EXEC [dbo].[set_fecha_editada_on_insert] @IdAlmacen;
            END
            GO
         ");
            // Procedimiento: insertar 0
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[set_fecha_editada_on_insert]
            @IdAlmacen INT
            AS
            BEGIN
            UPDATE Almacenes
            SET FechaEditada = 0
            WHERE IdAlmacen = @IdAlmacen AND FechaEditada IS NULL;
            END
            GO
         ");
            // Procedimiento: editar a 1
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[UpdateFechaEditada]
            @IdAlmacen INT
            AS
            BEGIN
            IF EXISTS (SELECT 1 FROM Almacenes WHERE FechaEditada = 1 AND IdAlmacen = @IdAlmacen)
            BEGIN
            RAISERROR('No se puede modificar los campos solo una vez.', 16, 1);
            RETURN;
            END
            UPDATE Almacenes
            SET FechaEditada = 1
            WHERE IdAlmacen = @IdAlmacen;
            END
            GO
         ");
            // Trigger: editar a 1
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateFechaEditada]
            ON  [dbo].[Almacenes]
            AFTER UPDATE
            AS
            BEGIN
            IF UPDATE(FechaLiberacion) OR UPDATE(FechaDevolicionTelas)
            BEGIN
            DECLARE @IdAlmacen INT;
            DECLARE inserted_cursor CURSOR FOR
            SELECT IdAlmacen FROM inserted;
            OPEN inserted_cursor;
            FETCH NEXT FROM inserted_cursor INTO @IdAlmacen;
            WHILE @@FETCH_STATUS = 0
            BEGIN
            EXEC [dbo].[UpdateFechaEditada] @IdAlmacen;
            FETCH NEXT FROM inserted_cursor INTO @IdAlmacen;
            END;
            CLOSE inserted_cursor;
            DEALLOCATE inserted_cursor;
            END
            END;
            GO
         ");

            //***etiquetas***
            // Procedimiento: editar a 0
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[set_fecha_etiquetas]
            @IdEtiquetas INT
            AS
            BEGIN
            UPDATE Etiquetas
            SET FechaEditada = 0
            WHERE IdEtiquetas = @IdEtiquetas AND FechaEditada IS NULL;
            END
            GO
         ");
            // Trigger: editar a 0
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trigger_invoke_set_fecha_etiquetas]
            ON [dbo].[Etiquetas]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdEtiquetas INT;
            SELECT @IdEtiquetas = IdEtiquetas FROM inserted;
            EXEC [dbo].[set_fecha_etiquetas] @IdEtiquetas;
            END
         ");
            // Procedimiento: editar a 1
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[UpdateFecha_Etiquetas]
            @IdEtiquetas INT
            AS
            BEGIN
            IF EXISTS (SELECT 1 FROM Etiquetas WHERE FechaEditada = 1 AND IdEtiquetas = @IdEtiquetas)
            BEGIN
            RAISERROR('No se puede modificar los campos solo una vez.', 16, 1);
            RETURN;
            END
            UPDATE Etiquetas
            SET FechaEditada = 1
            WHERE IdEtiquetas = @IdEtiquetas;
            END
            GO
         ");
            // Trigger: editar a 1
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateFecha_Etiquetas]
            ON [dbo].[Etiquetas]
            AFTER UPDATE
            AS
            BEGIN
            IF UPDATE(FechaEntregaMaquila ) OR UPDATE(FechaEntregaTerminado)
            BEGIN
            DECLARE @IdEtiquetas INT;
            DECLARE inserted_cursor CURSOR FOR
            SELECT IdEtiquetas FROM inserted;
            OPEN inserted_cursor;
            FETCH NEXT FROM inserted_cursor INTO @IdEtiquetas;
            WHILE @@FETCH_STATUS = 0
            BEGIN
            EXEC [dbo].[UpdateFecha_Etiquetas] @IdEtiquetas;
            FETCH NEXT FROM inserted_cursor INTO @IdEtiquetas;
            END;
            CLOSE inserted_cursor;
            DEALLOCATE inserted_cursor;
            END
            END;
            GO
         ");
            // Procedimiento: IdProceso
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[UpdateIdProcesoEtiquetas]
            @IdGeneral INT
            AS
            BEGIN
            UPDATE Generales
            SET IdProceso = '3'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Almacenes
            SET IdProceso = '3'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Cortes
            SET IdProceso = '3'
            WHERE IdGeneral = @IdGeneral;
			UPDATE CorteLaseres
            SET IdProceso = '3'
            WHERE IdGeneral = @IdGeneral;
            END;
            GO
         ");
            // Trigger: IdProceso
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateIdProcesoEtiqueta]
            ON [dbo].[Etiquetas]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdGeneral INT;
            SELECT TOP 1 @IdGeneral = IdGeneral
            FROM inserted;
            EXEC UpdateIdProcesoEtiquetas @IdGeneral;
            END;
            GO
         ");
            //***CORTE**
            // Procedimiento: editar a 0
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[set_fecha_Cortes]
            @IdCorte INT
            AS
            BEGIN
            UPDATE Cortes
            SET FechaEdita = 0
            WHERE IdCorte = @IdCorte AND FechaEdita IS NULL;
            END
            GO
         ");
            // Trigger: editar a 0
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trigger_invoke_set_fecha_Cortes]
            ON [dbo].[Cortes]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdCorte INT;
            SELECT @IdCorte = IdCorte FROM inserted;
            EXEC [dbo].[set_fecha_Cortes] @IdCorte;
            END
         ");
            // Procedimiento: editar a 1
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[UpdateFecha_Cortes]
            @IdCorte INT
            AS
            BEGIN
            IF EXISTS (SELECT 1 FROM Cortes WHERE FechaEdita = 1 AND IdCorte = @IdCorte)
            BEGIN
            RAISERROR('No se puede modificar los campos solo una vez.', 16, 1);
            RETURN;
            END
            UPDATE Cortes
            SET FechaEdita = 1
            WHERE IdCorte = @IdCorte;
            END
            GO
         ");
            // Trigger: editar a 1
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateFecha_Cortes]
            ON [dbo].[Cortes]
            AFTER UPDATE
            AS
            BEGIN
            IF UPDATE(FechaAVentas ) OR UPDATE(FechaEntrega)
            BEGIN
            DECLARE @IdCorte INT;
            DECLARE inserted_cursor CURSOR FOR
            SELECT IdCorte FROM inserted;
            OPEN inserted_cursor;
            FETCH NEXT FROM inserted_cursor INTO @IdCorte;
            WHILE @@FETCH_STATUS = 0
            BEGIN
            EXEC [dbo].[UpdateFecha_Cortes] @IdCorte;
            FETCH NEXT FROM inserted_cursor INTO @IdCorte;
            END;
            CLOSE inserted_cursor;
            DEALLOCATE inserted_cursor;
            END
            END;
            GO
         ");
            // Procedimiento: IdProceso
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[UpdateIdProcesoCortes]
            @IdGeneral INT
            AS
            BEGIN
            UPDATE Generales
            SET IdProceso = '4'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Almacenes
            SET IdProceso = '4'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Etiquetas
            SET IdProceso = '4'
            WHERE IdGeneral = @IdGeneral;
            END;
            GO
         ");
            // Trigger: IdProceso
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateIdProcesoCorte]
            ON [dbo].[Cortes]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdGeneral INT;
            SELECT TOP 1 @IdGeneral = IdGeneral
            FROM inserted;
            EXEC UpdateIdProcesoCortes @IdGeneral;
            END;
            GO
         ");
            //***CORTELASER**
            // Procedimiento: editar a 0
            migrationBuilder.Sql(@"
            Create PROCEDURE [dbo].[set_fecha_CorteLaseres]
            @IdCorteLaser INT
            AS
            BEGIN
            UPDATE CorteLaseres
            SET FechaEditada = 0
            WHERE IdCorteLaser = @IdCorteLaser AND FechaEditada IS NULL;
            END
            GO
         ");
            // Trigger: editar a 0
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trigger_invoke_set_fecha_CorteLaseres]
            ON [dbo].[CorteLaseres]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdCorteLaser INT;
            SELECT @IdCorteLaser = IdCorteLaser FROM inserted;
            EXEC [dbo].[set_fecha_CorteLaseres] @IdCorteLaser;
            END
         ");
            // Procedimiento: editar a 1
            migrationBuilder.Sql(@"
			CREATE PROCEDURE [dbo].[UpdateFecha_CorteLaseres]
            @IdCorteLaser INT
            AS
            BEGIN
            IF EXISTS (SELECT 1 FROM CorteLaseres WHERE FechaEditada = 1 AND IdCorteLaser = @IdCorteLaser)
            BEGIN
            RAISERROR('No se puede modificar los campos solo una vez.', 16, 1);
            RETURN;
            END
            UPDATE CorteLaseres
            SET FechaEditada = 1
            WHERE IdCorteLaser = @IdCorteLaser;
            END
            GO
         ");
            // Trigger: editar a 1
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateFecha_CorteLaseres]
            ON [dbo].[CorteLaseres]
            AFTER UPDATE
            AS
            BEGIN
            IF UPDATE(FechaEntrega)
            BEGIN
            DECLARE @IdCorteLaser INT;
            DECLARE inserted_cursor CURSOR FOR
            SELECT IdCorteLaser FROM inserted;
            OPEN inserted_cursor;
            FETCH NEXT FROM inserted_cursor INTO @IdCorteLaser;
            WHILE @@FETCH_STATUS = 0
            BEGIN
            EXEC [dbo].[UpdateFecha_CorteLaseres] @IdCorteLaser;
            FETCH NEXT FROM inserted_cursor INTO @IdCorteLaser;
            END;
            CLOSE inserted_cursor;
            DEALLOCATE inserted_cursor;
            END
            END;
            GO
         ");
            // Procedimiento: IdProceso
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[UpdateIdProcesoCorteLaseres]
            @IdGeneral INT
            AS
            BEGIN
            UPDATE Generales
            SET IdProceso = '5'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Almacenes
            SET IdProceso = '5'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Etiquetas
            SET IdProceso = '5'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Cortes
            SET IdProceso = '5'
            WHERE IdGeneral = @IdGeneral;
            END;
            GO
         ");
            // Trigger: IdProceso
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateIdProcesoCorteLaser]
            ON [dbo].[CorteLaseres]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdGeneral INT;
            SELECT TOP 1 @IdGeneral = IdGeneral
            FROM inserted;
            EXEC UpdateIdProcesoCortes @IdGeneral;
            END;
            GO
            ");
            //***SERIGRAFIA**
            // Procedimiento: editar a 0
            migrationBuilder.Sql(@"
            Create PROCEDURE [dbo].[set_fecha_Serigrafias]
            @IdSerigrafia INT
            AS
            BEGIN
            UPDATE Serigrafias
            SET FechaEditada = 0
            WHERE IdSerigrafia = @IdSerigrafia AND FechaEditada IS NULL;
            END
            GO
         ");
            // Trigger: editar a 0
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trigger_invoke_set_fecha_Serigrafias]
            ON [dbo].[Serigrafias]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdSerigrafia INT;
            SELECT @IdSerigrafia = IdSerigrafia FROM inserted;
            EXEC [dbo].[set_fecha_Serigrafias] @IdSerigrafia;
            END
         ");
            // Procedimiento: editar a 1
            migrationBuilder.Sql(@"
			CREATE PROCEDURE [dbo].[UpdateFecha_Serigrafias]
            @IdSerigrafia INT
            AS
            BEGIN
            IF EXISTS (SELECT 1 FROM Serigrafias WHERE FechaEditada = 1 AND IdSerigrafia = @IdSerigrafia)
            BEGIN
            RAISERROR('No se puede modificar los campos solo una vez.', 16, 1);
            RETURN;
            END
            UPDATE Serigrafias
            SET FechaEditada = 1
            WHERE IdSerigrafia = @IdSerigrafia;
            END
            GO
         ");
            // Trigger: editar a 1
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateFecha_Serigrafias]
            ON [dbo].[Serigrafias]
            AFTER UPDATE
            AS
            BEGIN
            IF UPDATE(FechaEntrega)
            BEGIN
            DECLARE @IdSerigrafia INT;
            DECLARE inserted_cursor CURSOR FOR
            SELECT IdSerigrafia FROM inserted;
            OPEN inserted_cursor;
            FETCH NEXT FROM inserted_cursor INTO @IdSerigrafia;
            WHILE @@FETCH_STATUS = 0
            BEGIN
            EXEC [dbo].[UpdateFecha_Serigrafias] @IdSerigrafia;
            FETCH NEXT FROM inserted_cursor INTO @IdSerigrafia;
            END;
            CLOSE inserted_cursor;
            DEALLOCATE inserted_cursor;
            END
            END;
            GO
         ");
            // Procedimiento: IdProceso
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[UpdateIdProcesoSerigrafias]
            @IdGeneral INT
            AS
            BEGIN
            UPDATE Generales
            SET IdProceso = '6'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Almacenes
            SET IdProceso = '6'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Etiquetas
            SET IdProceso = '6'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Cortes
            SET IdProceso = '6'
            WHERE IdGeneral = @IdGeneral;
            UPDATE CorteLaseres
            SET IdProceso = '6'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Bordados
            SET IdProceso = '6'
            WHERE IdGeneral = @IdGeneral;
			UPDATE Maquilas
            SET IdProceso = '6'
            WHERE IdGeneral = @IdGeneral;
			UPDATE Lavados
            SET IdProceso = '6'
            WHERE IdGeneral = @IdGeneral;
            END;
            GO
         ");
            // Trigger: IdProceso
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateIdProcesoSerigrafia]
            ON [dbo].[Serigrafias]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdGeneral INT;
            SELECT TOP 1 @IdGeneral = IdGeneral
            FROM inserted;
            EXEC UpdateIdProcesoSerigrafias @IdGeneral;
            END;
            GO
            ");
            //***Bordado**
            // Procedimiento: editar a 0
            migrationBuilder.Sql(@"
            Create PROCEDURE [dbo].[set_fecha_Bordados]
            @IdBordado INT
            AS
            BEGIN
            UPDATE Bordados
            SET FechaEditada = 0
            WHERE IdBordado = @IdBordado AND FechaEditada IS NULL;
            END
            GO
         ");
            // Trigger: editar a 0
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trigger_invoke_set_fecha_Bordados]
            ON [dbo].[Bordados]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdBordado INT;
            SELECT @IdBordado = IdBordado FROM inserted;
            EXEC [dbo].[set_fecha_Bordados] @IdBordado;
            END
         ");
            // Procedimiento: editar a 1
            migrationBuilder.Sql(@"
			CREATE PROCEDURE [dbo].[UpdateFecha_Bordados]
            @IdBordado INT
            AS
            BEGIN
            IF EXISTS (SELECT 1 FROM Bordados WHERE FechaEditada = 1 AND IdBordado = @IdBordado)
            BEGIN
            RAISERROR('No se puede modificar los campos solo una vez.', 16, 1);
            RETURN;
            END
            UPDATE Bordados
            SET FechaEditada = 1
            WHERE IdBordado = @IdBordado;
            END
            GO
         ");
            // Trigger: editar a 1
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateFecha_Bordados]
            ON [dbo].[Bordados]
            AFTER UPDATE
            AS
            BEGIN
            IF UPDATE(FechaEntrega)
            BEGIN
            DECLARE @IdBordado INT;
            DECLARE inserted_cursor CURSOR FOR
            SELECT IdBordado FROM inserted;
            OPEN inserted_cursor;
            FETCH NEXT FROM inserted_cursor INTO @IdBordado;
            WHILE @@FETCH_STATUS = 0
            BEGIN
            EXEC [dbo].[UpdateFecha_Bordados] @IdBordado;
            FETCH NEXT FROM inserted_cursor INTO @IdBordado;
            END;
            CLOSE inserted_cursor;
            DEALLOCATE inserted_cursor;
            END
            END;
            GO
         ");
            // Procedimiento: IdProceso
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[UpdateIdProcesoBordados]
            @IdGeneral INT
            AS
            BEGIN
            UPDATE Generales
            SET IdProceso = '7'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Almacenes
            SET IdProceso = '7'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Etiquetas
            SET IdProceso = '7'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Cortes
            SET IdProceso = '7'
            WHERE IdGeneral = @IdGeneral;
            UPDATE CorteLaseres
            SET IdProceso = '7'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Serigrafias
            SET IdProceso = '7'
            WHERE IdGeneral = @IdGeneral;
			UPDATE Maquilas
            SET IdProceso = '7'
            WHERE IdGeneral = @IdGeneral;
			UPDATE Lavados
            SET IdProceso = '7'
            WHERE IdGeneral = @IdGeneral;
            END;
            GO
         ");
            // Trigger: IdProceso
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateIdProcesoBordado]
            ON [dbo].[Bordados]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdGeneral INT;
            SELECT TOP 1 @IdGeneral = IdGeneral
            FROM inserted;
            EXEC UpdateIdProcesoBordados @IdGeneral;
            END;
            GO
            ");
            //***Maquila**
            // Procedimiento: editar a 0
            migrationBuilder.Sql(@"
            Create PROCEDURE [dbo].[set_fecha_Maquilas]
            @IdMaquila INT
            AS
            BEGIN
            UPDATE Maquilas
            SET FechaEditada = 0
            WHERE IdMaquila = @IdMaquila AND FechaEditada IS NULL;
            END
            GO
         ");
            // Trigger: editar a 0
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trigger_invoke_set_fecha_Maquilas]
            ON [dbo].[Maquilas]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdMaquila INT;
            SELECT @IdMaquila = IdMaquila FROM inserted;
            EXEC [dbo].[set_fecha_Maquilas] @IdMaquila;
            END
         ");
            // Procedimiento: editar a 1
            migrationBuilder.Sql(@"
			CREATE PROCEDURE [dbo].[UpdateFecha_Maquilas]
            @IdMaquila INT
            AS
            BEGIN
            IF EXISTS (SELECT 1 FROM Maquilas WHERE FechaEditada = 1 AND IdMaquila = @IdMaquila)
            BEGIN
            RAISERROR('No se puede modificar los campos solo una vez.', 16, 1);
            RETURN;
            END
            UPDATE Maquilas
            SET FechaEditada = 1
            WHERE IdMaquila = @IdMaquila;
            END
            GO
         ");
            // Trigger: editar a 1
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateFecha_Maquilas]
            ON [dbo].[Maquilas]
            AFTER UPDATE
            AS
            BEGIN
            IF UPDATE(FechaEntrega)
            BEGIN
            DECLARE @IdMaquila INT;
            DECLARE inserted_cursor CURSOR FOR
            SELECT IdMaquila FROM inserted;
            OPEN inserted_cursor;
            FETCH NEXT FROM inserted_cursor INTO @IdMaquila;
            WHILE @@FETCH_STATUS = 0
            BEGIN
            EXEC [dbo].[UpdateFecha_Maquilas] @IdMaquila;
            FETCH NEXT FROM inserted_cursor INTO @IdMaquila;
            END;
            CLOSE inserted_cursor;
            DEALLOCATE inserted_cursor;
            END
            END;
            GO
         ");
            // Procedimiento: IdProceso
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[UpdateIdProcesoMaquilas]
            @IdGeneral INT
            AS
            BEGIN
            UPDATE Generales
            SET IdProceso = '8'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Almacenes
            SET IdProceso = '8'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Etiquetas
            SET IdProceso = '8'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Cortes
            SET IdProceso = '8'
            WHERE IdGeneral = @IdGeneral;
            UPDATE CorteLaseres
            SET IdProceso = '8'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Serigrafias
            SET IdProceso = '8'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Bordados
            SET IdProceso = '8'
            WHERE IdGeneral = @IdGeneral;
			UPDATE Lavados
            SET IdProceso = '8'
            WHERE IdGeneral = @IdGeneral;
            END;
            GO
         ");
            // Trigger: IdProceso
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateIdProcesoMaquila]
            ON [dbo].[Maquilas]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdGeneral INT;
            SELECT TOP 1 @IdGeneral = IdGeneral
            FROM inserted;
            EXEC UpdateIdProcesoMaquilas @IdGeneral;
            END;
            GO
            ");
            //***Lavado**
            // Procedimiento: editar a 0
            migrationBuilder.Sql(@"
            Create PROCEDURE [dbo].[set_fecha_Lavados]
            @IdLavado INT
            AS
            BEGIN
            UPDATE Lavados
            SET FechaEditada = 0
            WHERE IdLavado = @IdLavado AND FechaEditada IS NULL;
            END
            GO
         ");
            // Trigger: editar a 0
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trigger_invoke_set_fecha_Lavados]
            ON [dbo].[Lavados]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdLavado INT;
            SELECT @IdLavado= IdLavado FROM inserted;
            EXEC [dbo].[set_fecha_Lavados] @IdLavado;
            END
         ");
            // Procedimiento: editar a 1
            migrationBuilder.Sql(@"
			CREATE PROCEDURE [dbo].[UpdateFecha_Lavados]
            @IdLavado INT
            AS
            BEGIN
            IF EXISTS (SELECT 1 FROM Lavados WHERE FechaEditada = 1 AND IdLavado = @IdLavado)
            BEGIN
            RAISERROR('No se puede modificar los campos solo una vez.', 16, 1);
            RETURN;
            END
            UPDATE Lavados
            SET FechaEditada = 1
            WHERE IdLavado = @IdLavado;
            END
            GO
         ");
            // Trigger: editar a 1
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateFecha_Lavados]
            ON [dbo].[Lavados]
            AFTER UPDATE
            AS
            BEGIN
            IF UPDATE(FechaEntrega)
            BEGIN
            DECLARE @IdLavado INT;
            DECLARE inserted_cursor CURSOR FOR
            SELECT IdLavado FROM inserted;
            OPEN inserted_cursor;
            FETCH NEXT FROM inserted_cursor INTO @IdLavado;
            WHILE @@FETCH_STATUS = 0
            BEGIN
            EXEC [dbo].[UpdateFecha_Lavados] @IdLavado;
            FETCH NEXT FROM inserted_cursor INTO @IdLavado;
            END;
            CLOSE inserted_cursor;
            DEALLOCATE inserted_cursor;
            END
            END;
            GO
         ");
            // Procedimiento: IdProceso
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[UpdateIdProcesoLavados]
            @IdGeneral INT
            AS
            BEGIN
            UPDATE Generales
            SET IdProceso = '9'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Almacenes
            SET IdProceso = '9'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Etiquetas
            SET IdProceso = '9'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Cortes
            SET IdProceso = '9'
            WHERE IdGeneral = @IdGeneral;
            UPDATE CorteLaseres
            SET IdProceso = '9'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Serigrafias
            SET IdProceso = '9'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Bordados
            SET IdProceso = '9'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Maquilas
            SET IdProceso = '9'
            WHERE IdGeneral = @IdGeneral;
            END;
            GO
         ");
            // Trigger: IdProceso
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateIdProcesoLavado]
            ON [dbo].[Lavados]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdGeneral INT;
            SELECT TOP 1 @IdGeneral = IdGeneral
            FROM inserted;
            EXEC UpdateIdProcesoLavados @IdGeneral;
            END;
            GO
            ");
            //***Calidad** queda pendiente los procedures o tiene fechaeditada            
            // Procedimiento: editar a 0
            migrationBuilder.Sql(@"
            Create PROCEDURE [dbo].[set_fecha_Calidades]
            @IdCalidad INT
            AS
            BEGIN
            UPDATE Calidades
            SET FechaEditada = 0
            WHERE IdCalidad = @IdCalidad AND FechaEditada IS NULL;
            END
            GO
         ");
            // Trigger: editar a 0
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trigger_invoke_set_fecha_Calidades]
            ON [dbo].[Calidades]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdCalidad INT;
            SELECT @IdCalidad = IdCalidad FROM inserted;
            EXEC [dbo].[set_fecha_Calidades] @IdCalidad;
            END
         ");
            // Procedimiento: editar a 1
            migrationBuilder.Sql(@"
			CREATE PROCEDURE [dbo].[UpdateFecha_Calidades]
            @IdCalidad INT
            AS
            BEGIN
            IF EXISTS (SELECT 1 FROM Calidades WHERE FechaEditada = 1 AND IdCalidad = @IdCalidad)
            BEGIN
            RAISERROR('No se puede modificar los campos solo una vez.', 16, 1);
            RETURN;
            END
            UPDATE Calidades
            SET FechaEditada = 1
            WHERE IdCalidad = @IdCalidad;
            END
            GO
         ");
            // Trigger: editar a 1
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateFecha_Calidades]
            ON [dbo].[Calidades]
            AFTER UPDATE
            AS
            BEGIN
            IF UPDATE(FechaEnvioMaquila) OR UPDATE(FechaRecepcionRechazo)
            BEGIN
            DECLARE @IdCalidad INT;
            DECLARE inserted_cursor CURSOR FOR
            SELECT IdCalidad FROM inserted;
            OPEN inserted_cursor;
            FETCH NEXT FROM inserted_cursor INTO @IdCalidad;
            WHILE @@FETCH_STATUS = 0
            BEGIN
            EXEC [dbo].[UpdateFecha_Calidades] @idCalidad;
            FETCH NEXT FROM inserted_cursor INTO @IdCalidad;
            END;
            CLOSE inserted_cursor;
            DEALLOCATE inserted_cursor;
            END
            END;
            GO
         ");
            // Procedimiento: IdProceso
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[UpdateIdProcesoCalidades]
            @IdGeneral INT
            AS
            BEGIN
            UPDATE Generales
            SET IdProceso = '10'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Almacenes
            SET IdProceso = '10'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Etiquetas
            SET IdProceso = '10'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Cortes
            SET IdProceso = '10'
            WHERE IdGeneral = @IdGeneral;
            UPDATE CorteLaseres
            SET IdProceso = '10'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Serigrafias
            SET IdProceso = '10'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Bordados
            SET IdProceso = '10'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Maquilas
            SET IdProceso = '10'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Lavados
            SET IdProceso = '10'
            WHERE IdGeneral = @IdGeneral;
            END;
            GO
         ");
            // Trigger: IdProceso
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateIdProcesoCalidad]
            ON [dbo].[Calidades]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdGeneral INT;
            SELECT TOP 1 @IdGeneral = IdGeneral
            FROM inserted;
            EXEC UpdateIdProcesoCalidades @IdGeneral;
            END;
            GO
            ");
            //***Terminado**            
            // Procedimiento: editar a 0
            migrationBuilder.Sql(@"
            Create PROCEDURE [dbo].[set_fecha_Terminados]
            @IdTerminado INT
            AS
            BEGIN
            UPDATE Terminados
            SET FechaEditada = 0
            WHERE IdTerminado = @IdTerminado AND FechaEditada IS NULL;
            END
            GO
         ");
            // Trigger: editar a 0
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trigger_invoke_set_fecha_Terminados]
            ON [dbo].[Terminados]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdTerminado INT;
            SELECT @IdTerminado = IdTerminado FROM inserted;
            EXEC [dbo].[set_fecha_Terminados] @IdTerminado;
            END
         ");
            // Procedimiento: editar a 1
            migrationBuilder.Sql(@"
			CREATE PROCEDURE [dbo].[UpdateFecha_Terminados]
            @IdTerminado INT
            AS
            BEGIN
            IF EXISTS (SELECT 1 FROM Terminados WHERE FechaEditada = 1 AND IdTerminado = @IdTerminado)
            BEGIN
            RAISERROR('No se puede modificar los campos solo una vez.', 16, 1);
            RETURN;
            END
            UPDATE Terminados
            SET FechaEditada = 1
            WHERE IdTerminado = @IdTerminado;
            END
            GO
         ");
            // Trigger: editar a 1
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateFecha_Terminados]
            ON [dbo].[Terminados]
            AFTER UPDATE
            AS
            BEGIN
            IF UPDATE(FechaEntrega)
            BEGIN
            DECLARE @IdTerminado INT;
            DECLARE inserted_cursor CURSOR FOR
            SELECT IdTerminado FROM inserted;
            OPEN inserted_cursor;
            FETCH NEXT FROM inserted_cursor INTO @IdTerminado;
            WHILE @@FETCH_STATUS = 0
            BEGIN
            EXEC [dbo].[UpdateFecha_Terminados] @idTerminado;
            FETCH NEXT FROM inserted_cursor INTO @IdTerminado;
            END;
            CLOSE inserted_cursor;
            DEALLOCATE inserted_cursor;
            END
            END;
            GO
         ");
            // Procedimiento: IdProceso
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[UpdateIdProcesoTerminados]
            @IdGeneral INT
            AS
            BEGIN
            UPDATE Generales
            SET IdProceso = '11'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Almacenes
            SET IdProceso = '11'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Etiquetas
            SET IdProceso = '11'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Cortes
            SET IdProceso = '11'
            WHERE IdGeneral = @IdGeneral;
            UPDATE CorteLaseres
            SET IdProceso = '11'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Serigrafias
            SET IdProceso = '11'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Bordados
            SET IdProceso = '11'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Maquilas
            SET IdProceso = '11'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Lavados
            SET IdProceso = '11'
            WHERE IdGeneral = @IdGeneral;
            UPDATE Calidades
            SET IdProceso = '11'
            WHERE IdGeneral = @IdGeneral;
            END;
            GO
         ");
            // Trigger: IdProceso
            migrationBuilder.Sql(@"
            CREATE TRIGGER [dbo].[trg_UpdateIdProcesoTerminado]
            ON [dbo].[Terminados]
            AFTER INSERT
            AS
            BEGIN
            DECLARE @IdGeneral INT;
            SELECT TOP 1 @IdGeneral = IdGeneral
            FROM inserted;
            EXEC UpdateIdProcesoTerminados @IdGeneral;
            END;
            GO
            ");
            migrationBuilder.Sql(@"
            INSERT INTO ProcesoActuales ([Area])
            VALUES 
            ('General'),
            ('Almacen'),
            ('Etiquetas'),
            ('Corte'),
            ('CorteLaser'),
            ('Serigrafia'),
            ('Bordado'),
            ('Maquila'),
            ('Lavado'),
            ('Calidad'),
            ('Terminado');
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar los triggers y procedimientos si se revierte la migración
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateFechaEditada]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trigger_actualizar_proceso_actual]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trigger_invoke_set_fecha_editada_on_insert]");

            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateFechaEditada]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateGeneralesProceso]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[set_fecha_editada_on_insert]");
            //etiquetas
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateFecha_Etiquetas]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateIdProcesoEtiqueta]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trigger_invoke_set_fecha_etiquetas]");

            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateFecha_Etiquetas]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[set_fecha_etiquetas]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateIdProcesoEtiquetas]");
            //Cortes
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateFecha_Cortes]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateIdProcesoCorte]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trigger_invoke_set_fecha_Cortes]");

            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateFecha_Cortes]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[set_fecha_Cortes]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateIdProcesoCortes]");
            //CorteLaseres
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateFecha_CorteLaseres]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateIdProcesoCorteLaser]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trigger_invoke_set_fecha_CorteLaseres]");

            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateFecha_CorteLaseres]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[set_fecha_CorteLaseres]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateIdProcesoCorteLaseres]");
            //Serigrafia
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateFecha_Serigrafias]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateIdProcesoSerigrafia]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trigger_invoke_set_fecha_CorteSerigrafias]");

            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateFecha_Serigrafias]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[set_fecha_Serigrafias]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateIdProcesoSerigrafias]");
            //Bordado
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateFecha_Bordados]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateIdProcesoBordado]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trigger_invoke_set_fecha_Bordados]");

            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateFecha_Bordados]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[set_fecha_Bordados]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateIdProcesoBordados]");
            //Maquila
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateFecha_Maquilas]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateIdProcesoMaquila]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trigger_invoke_set_fecha_Maquilas]");

            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateFecha_Maquilas]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[set_fecha_Maquilas]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateIdProcesoMaquilas]");
            //Lavado
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateFecha_Lavados]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateIdProcesoLavado]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trigger_invoke_set_fecha_Lavados]");

            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateFecha_Lavados]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[set_fecha_Lavados]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateIdProcesoLavados]");
            //Calidad
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateFecha_Calidades]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateIdProcesoCalidad]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trigger_invoke_set_fecha_Calidades]");

            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateFecha_Calidades]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[set_fecha_Calidades]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateIdProcesoCalidades]");
            //Terminado
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateFecha_Terminados]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trg_UpdateIdProcesoTerminado]");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[trigger_invoke_set_fecha_Terminados]");

            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateFecha_Terminados]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[set_fecha_Terminados]");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateIdProcesoTerminados]");
        }
    }
}
