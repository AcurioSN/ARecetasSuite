Imports Data
Public Class NRecetas
    Dim obj As New Data.DRecetas
    Public Function Listar_CentroCostos() As DataSet
        Dim ds As New DataSet
        ds = obj.Listar_CentroCostos()
        Return ds
    End Function
    Public Function Registra_grupo(ByVal cod_centro As String, ByVal desc_grupo As String, ByVal id_usuario As Integer) As Boolean

        obj.Registra_grupo(cod_centro, desc_grupo, id_usuario)
        Return True
    End Function
    Public Function Listar_Grupos_Recetas(ByVal id_usuario As Integer) As DataSet
        Dim ds As New DataSet
        ds = obj.Lista_Grupos_Recetas(id_usuario)
        Return ds
    End Function

    Public Function Lista_Productos_centro_almacen_Reservar(ByVal centro As String, ByVal cod_producto As String, ByVal desc_producto As String) As DataSet
        Dim ds As New DataSet
        ds = obj.Lista_Productos_centro_almacen_Reservar(centro, cod_producto, desc_producto)
        Return ds
    End Function

    Public Function Lista_detalle_insumos_seleccionado(ByVal Material As String, ByVal centro As String) As DataSet
        Dim ds As New DataSet
        ds = obj.Lista_detalle_insumos_seleccionado(Material, centro)
        Return ds
    End Function

    Public Function Registra_manual_receta(ByVal DescripcionDocumento As String,
                                       ByVal centro_costos As String,
                                       ByVal cod_sap As String,
                                       ByVal id_usuario As Integer,
                                       ByVal id_grupo As Integer,
                                       ByVal preparacion As String,
                                       ByVal desc_sap As String) As Integer

        Dim idManual As Integer = obj.Registra_manual_receta(DescripcionDocumento,
                                                             centro_costos,
                                                             cod_sap,
                                                             id_usuario,
                                                             id_grupo, preparacion, desc_sap)

        Return idManual

    End Function

    Public Function Registra_detalle_manual_receta(ByVal id_manual As String,
                                       ByVal codigoPadre As String,
                                       ByVal descripcionPadre As String,
                                       ByVal codigo As String,
                                       ByVal descripcion As String,
                                       ByVal unidadMedida As String,
                                       ByVal cantidad As Decimal,
                                       ByVal unidadMedida_cabecera As String,
                                       ByVal cantidad_cabecera As Decimal) As Boolean

        obj.Registra_detalle_manual_receta(id_manual, codigoPadre, descripcionPadre, codigo, descripcion, unidadMedida, cantidad, unidadMedida_cabecera, cantidad_cabecera)
        Return True
    End Function

    Public Function Lista_datos_excel(ByVal id_grupo As Integer) As DataSet
        Dim ds As New DataSet
        ds = obj.Lista_datos_excel(id_grupo)
        Return ds
    End Function
    Public Function Listar_Manual_Recetas(ByVal id_usuario As Integer) As DataSet
        Dim ds As New DataSet
        ds = obj.Lista_manuales_Recetas(id_usuario)
        Return ds
    End Function

    Public Function Listar_Manual_Grupo(ByVal id_grupo As Integer) As DataSet
        Dim ds As New DataSet
        ds = obj.Lista_manuales_Grupo(id_grupo)
        Return ds
    End Function

    Public Function Busqueda_Manuales(ByVal cod_manual As String, ByVal desc_manual As String) As DataSet
        Dim ds As New DataSet
        ds = obj.Busqueda_Manuales(cod_manual, desc_manual)
        Return ds
    End Function

    Public Function Modifica_Grupo(ByVal id_grupo As Integer, ByVal desc_grupo As String, ByVal id_usuario As Integer) As Boolean

        obj.Modifica_Grupo(id_grupo, desc_grupo, id_usuario)
        Return True
    End Function

    Public Function Modifica_Insertar_Manual_Grupo(ByVal idGrupo As Integer, ByVal idManual As Integer) As Boolean

        obj.Modifica_Insertar_Manual_Grupo(idGrupo, idManual)
        Return True
    End Function

    Public Function grupos_manuales_eliminar(ByVal id_manual As Integer) As DataSet
        Dim ds As New DataSet
        ds = obj.grupos_manuales_eliminar(id_manual)
        Return ds
    End Function

    Public Function Eliminar_manual(ByVal id_manual As Integer, ByVal id_usuario As Integer) As Boolean

        obj.Eliminar_Manual(id_manual, id_usuario)
        Return True
    End Function

    Public Function Lista_manuales_grupo_eliminar(ByVal id_grupo As Integer) As DataSet
        Dim ds As New DataSet
        ds = obj.Lista_manuales_grupo_eliminar(id_grupo)
        Return ds
    End Function
    Public Function Eliminar_grupo(ByVal id_grupo As Integer, ByVal id_usuario As Integer) As Boolean

        obj.Eliminar_Grupo(id_grupo, id_usuario)
        Return True
    End Function

    Public Function validar_acceso(ByVal usuario As String) As DataSet
        Dim ds As New DataSet
        ds = obj.validar_acceso(usuario)
        Return ds
    End Function

    Public Function Registra_archivo_manual(ByVal id_manual As Integer, ByVal nombre_archivo As String) As Boolean

        obj.Registra_archivo_manual(id_manual, nombre_archivo)
        Return True
    End Function
    Public Function ExisteTokenActivo(ByVal token As String) As DataSet
        Dim ds As New DataSet
        ds = obj.ExisteTokenActivo(token)
        Return ds
    End Function

    Public Function CerrarSesionGlobal(ByVal tokenGlobal As String) As Boolean

        obj.CerrarSesionGlobal(tokenGlobal)
        Return True
    End Function

    Public Function Registro_SesionSistema(ByVal tokenGlobal As String, ByVal usuario As String, ByVal CodigoSistema As String, ByVal PaginaAcceso As String) As Boolean

        obj.Registro_SesionSistema(tokenGlobal, usuario, CodigoSistema, PaginaAcceso)
        Return True
    End Function

End Class
