Imports System.Data.SqlClient
Imports System.Configuration

Public Class DRecetas
	Public Function Listar_CentroCostos() As DataSet
		Dim ds As New DataSet()

		Try
			Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

			Using cn As New SqlConnection(strConnString)
				Using cmd As New SqlCommand("sp_arrecetas_listar_CentroCostos", cn)
					cmd.CommandType = CommandType.StoredProcedure
					cmd.CommandTimeout = 0

					' Llenar el DataSet usando SqlDataAdapter
					Using da As New SqlDataAdapter(cmd)
						da.Fill(ds)
					End Using
				End Using
			End Using

		Catch ex As Exception
			' Aquí puedes registrar el error antes de relanzarlo
			' LogError(ex.Message)
			Throw ' Lanza el error para que sea manejado en niveles superiores
		End Try

		Return ds
	End Function

    Public Function Registra_grupo(ByVal cod_centro As String, ByVal desc_grupo As String, ByVal id_usuario As Integer) As Boolean
        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arrecetas_registra_grupo", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Agregar parámetros con tipo y tamaño
                    cmd.Parameters.Add("@cod_centro", SqlDbType.VarChar, 50).Value = cod_centro
                    cmd.Parameters.Add("@desc_grupo", SqlDbType.VarChar, 500).Value = desc_grupo
                    cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario

                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True
        Catch ex As Exception
            ' Aquí puedes registrar el error antes de relanzarlo
            ' LogError(ex.Message) 
            Throw ' Lanza el error para que sea manejado en niveles superiores
        End Try
    End Function

    Public Function Lista_Grupos_Recetas(ByVal id_usuario As Integer) As DataSet
        Dim ds As New DataSet()

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arrecetas_lista_grupo", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Definir parámetro con tipo y tamaño
                    cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario

                    ' Llenar el DataSet usando SqlDataAdapter
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(ds)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ' Registrar el error si es necesario
            ' LogError(ex.Message) 
            Throw ' Relanzar el error para ser manejado en un nivel superior
        End Try

        Return ds
    End Function

    Public Function Lista_Productos_centro_almacen_Reservar(ByVal centro As String, ByVal cod_producto As String, ByVal desc_producto As String) As DataSet
        Dim ds As New DataSet()

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                'Using cmd As New SqlCommand("sp_arencocina_lista_productos_centro_almacen_SAP", cn) '21/05/2025
                Using cmd As New SqlCommand("sp_arrecetas_lista_productos_centro_almacen_Reservar_SAP_01", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Definir parámetros con tipo y tamaño
                    cmd.Parameters.Add("@centro", SqlDbType.VarChar, 50).Value = centro
                    cmd.Parameters.Add("@cod_producto", SqlDbType.VarChar, 50).Value = cod_producto
                    cmd.Parameters.Add("@desc_producto", SqlDbType.VarChar, 100).Value = desc_producto

                    ' Llenar el DataSet usando SqlDataAdapter
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(ds)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ' Aquí puedes registrar el error antes de relanzarlo
            ' LogError(ex.Message) 
            Throw ' Lanza el error para que sea manejado en niveles superiores
        End Try

        Return ds
    End Function


    Public Function Lista_detalle_insumos_seleccionado(ByVal Material As String, ByVal centro As String) As DataSet
        Dim ds As New DataSet()

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                'Using cmd As New SqlCommand("sp_arencocina_lista_productos_centro_almacen_SAP", cn) '21/05/2025
                Using cmd As New SqlCommand("sp_arrecetas_traer_insumo_sap", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Definir parámetros con tipo y tamaño
                    cmd.Parameters.Add("@Material", SqlDbType.VarChar, 50).Value = Material
                    cmd.Parameters.Add("@Centro", SqlDbType.VarChar, 50).Value = centro

                    ' Llenar el DataSet usando SqlDataAdapter
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(ds)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ' Aquí puedes registrar el error antes de relanzarlo
            ' LogError(ex.Message) 
            Throw ' Lanza el error para que sea manejado en niveles superiores
        End Try

        Return ds
    End Function

    Public Function Registra_manual_receta(ByVal DescripcionDocumento As String,
                                        ByVal centro_costos As String,
                                        ByVal cod_sap As String,
                                        ByVal id_usuario As Integer,
                                        ByVal id_grupo As Integer,
                                        ByVal preparacion As String,
                                        ByVal desc_sap As String) As Integer

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arrecetas_registra_manual_receta", cn)

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Parámetros normales
                    cmd.Parameters.Add("@DescripcionDocumento", SqlDbType.VarChar, 500).Value = DescripcionDocumento
                    cmd.Parameters.Add("@centro_costos", SqlDbType.VarChar, 25).Value = centro_costos
                    cmd.Parameters.Add("@cod_sap", SqlDbType.VarChar, 150).Value = cod_sap
                    cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario
                    cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo
                    cmd.Parameters.Add("@preparacion", SqlDbType.VarChar, 5000).Value = preparacion
                    cmd.Parameters.Add("@desc_sap", SqlDbType.VarChar, 500).Value = desc_sap

                    ' Parámetro OUTPUT
                    Dim paramIdManual As New SqlParameter("@id_manual", SqlDbType.Int)
                    paramIdManual.Direction = ParameterDirection.Output
                    cmd.Parameters.Add(paramIdManual)

                    cn.Open()
                    cmd.ExecuteNonQuery()

                    ' Obtener valor generado
                    Dim idManualGenerado As Integer = Convert.ToInt32(cmd.Parameters("@id_manual").Value)

                    Return idManualGenerado

                End Using
            End Using

        Catch ex As Exception
            Throw
        End Try

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

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arrecetas_registra_detalle_manual_receta", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Agregar parámetros con tipo y tamaño
                    cmd.Parameters.Add("@id_manual", SqlDbType.Int).Value = id_manual
                    cmd.Parameters.Add("@codigoPadre", SqlDbType.VarChar, 150).Value = codigoPadre
                    cmd.Parameters.Add("@descripcionPadre", SqlDbType.VarChar, 500).Value = descripcionPadre
                    cmd.Parameters.Add("@codigo", SqlDbType.VarChar, 150).Value = codigo
                    cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 500).Value = descripcion
                    cmd.Parameters.Add("@unidadMedida", SqlDbType.VarChar, 25).Value = unidadMedida
                    cmd.Parameters.Add("@cantidad", SqlDbType.Decimal).Value = cantidad
                    cmd.Parameters.Add("@unidad_medida_cabecera", SqlDbType.VarChar, 25).Value = unidadMedida_cabecera
                    cmd.Parameters.Add("@cantidad_cabecera", SqlDbType.Decimal).Value = cantidad_cabecera



                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True
        Catch ex As Exception
            ' Aquí puedes registrar el error antes de relanzarlo
            ' LogError(ex.Message) 
            Throw ' Lanza el error para que sea manejado en niveles superiores
        End Try
    End Function

    Public Function Lista_datos_excel(ByVal id_grupo As Integer) As DataSet
        Dim ds As New DataSet()

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                'Using cmd As New SqlCommand("sp_arencocina_lista_productos_centro_almacen_SAP", cn) '21/05/2025
                Using cmd As New SqlCommand("sp_arrecetas_datos_excel", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Definir parámetros con tipo y tamaño
                    cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo

                    ' Llenar el DataSet usando SqlDataAdapter
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(ds)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ' Aquí puedes registrar el error antes de relanzarlo
            ' LogError(ex.Message) 
            Throw ' Lanza el error para que sea manejado en niveles superiores
        End Try

        Return ds
    End Function

    Public Function Lista_manuales_Recetas(ByVal id_usuario As Integer) As DataSet
        Dim ds As New DataSet()

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arrecetas_lista_manuales", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Definir parámetro con tipo y tamaño
                    cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario

                    ' Llenar el DataSet usando SqlDataAdapter
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(ds)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ' Registrar el error si es necesario
            ' LogError(ex.Message) 
            Throw ' Relanzar el error para ser manejado en un nivel superior
        End Try

        Return ds
    End Function

    Public Function Lista_manuales_Grupo(ByVal id_grupo As Integer) As DataSet
        Dim ds As New DataSet()

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arrecetas_lista_manuales_grupo", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Definir parámetro con tipo y tamaño
                    cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo

                    ' Llenar el DataSet usando SqlDataAdapter
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(ds)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ' Registrar el error si es necesario
            ' LogError(ex.Message) 
            Throw ' Relanzar el error para ser manejado en un nivel superior
        End Try

        Return ds
    End Function


    Public Function Busqueda_Manuales(ByVal cod_manual As String, ByVal desc_manual As String) As DataSet
        Dim ds As New DataSet()

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arrecetas_busqueda_manuales", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Definir parámetro con tipo y tamaño
                    cmd.Parameters.Add("@cod_manual", SqlDbType.VarChar).Value = cod_manual
                    cmd.Parameters.Add("@desc_manual", SqlDbType.VarChar).Value = desc_manual

                    ' Llenar el DataSet usando SqlDataAdapter
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(ds)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ' Registrar el error si es necesario
            ' LogError(ex.Message) 
            Throw ' Relanzar el error para ser manejado en un nivel superior
        End Try

        Return ds
    End Function

    Public Function Modifica_Grupo(ByVal id_grupo As Integer, ByVal desc_grupo As String, ByVal id_usuario As Integer) As Boolean

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arrecetas_modifica_grupo", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Agregar parámetros con tipo y tamaño
                    cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo
                    cmd.Parameters.Add("@desc_grupo", SqlDbType.VarChar, 250).Value = desc_grupo
                    cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario

                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True
        Catch ex As Exception
            ' Aquí puedes registrar el error antes de relanzarlo
            ' LogError(ex.Message) 
            Throw ' Lanza el error para que sea manejado en niveles superiores
        End Try
    End Function

    Public Function Modifica_Insertar_Manual_Grupo(ByVal idGrupo As Integer, ByVal idManual As Integer) As Boolean

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arrecetas_modifica_grupo_manual", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Agregar parámetros con tipo y tamaño
                    cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = idGrupo
                    cmd.Parameters.Add("@id_manual", SqlDbType.Int).Value = idManual

                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True
        Catch ex As Exception
            ' Aquí puedes registrar el error antes de relanzarlo
            ' LogError(ex.Message) 
            Throw ' Lanza el error para que sea manejado en niveles superiores
        End Try
    End Function

    Public Function grupos_manuales_eliminar(ByVal id_manual As Integer) As DataSet
        Dim ds As New DataSet()

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arrecetas_grupos_manuales_eliminar", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Definir parámetro con tipo y tamaño
                    cmd.Parameters.Add("@id_manual", SqlDbType.Int).Value = id_manual

                    ' Llenar el DataSet usando SqlDataAdapter
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(ds)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ' Registrar el error si es necesario
            ' LogError(ex.Message) 
            Throw ' Relanzar el error para ser manejado en un nivel superior
        End Try

        Return ds
    End Function

    Public Function Eliminar_Manual(ByVal id_manual As Integer, ByVal id_usuario As Integer) As Boolean

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arrecetas_eliminar_manual", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Agregar parámetros con tipo y tamaño
                    cmd.Parameters.Add("@id_manual", SqlDbType.Int).Value = id_manual
                    cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario

                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True
        Catch ex As Exception
            ' Aquí puedes registrar el error antes de relanzarlo
            ' LogError(ex.Message) 
            Throw ' Lanza el error para que sea manejado en niveles superiores
        End Try
    End Function

    Public Function Lista_manuales_grupo_eliminar(ByVal id_grupo As Integer) As DataSet
        Dim ds As New DataSet()

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arrecetas_lista_manuales_grupo_eliminar", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Definir parámetro con tipo y tamaño
                    cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo

                    ' Llenar el DataSet usando SqlDataAdapter
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(ds)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ' Registrar el error si es necesario
            ' LogError(ex.Message) 
            Throw ' Relanzar el error para ser manejado en un nivel superior
        End Try

        Return ds
    End Function

    Public Function Eliminar_Grupo(ByVal id_grupo As Integer, ByVal id_usuario As Integer) As Boolean

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arrecetas_eliminar_grupo", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Agregar parámetros con tipo y tamaño
                    cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo
                    cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario

                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True
        Catch ex As Exception
            ' Aquí puedes registrar el error antes de relanzarlo
            ' LogError(ex.Message) 
            Throw ' Lanza el error para que sea manejado en niveles superiores
        End Try
    End Function

    Public Function validar_acceso(ByVal usuario As String) As DataSet
        Dim ds As New DataSet()

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arrecetas_inicio_sesion", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Definir parámetro con tipo y tamaño
                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 150).Value = usuario

                    ' Llenar el DataSet usando SqlDataAdapter
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(ds)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ' Registrar el error si es necesario
            ' LogError(ex.Message) 
            Throw ' Relanzar el error para ser manejado en un nivel superior
        End Try

        Return ds
    End Function
    Public Function Registra_archivo_manual(ByVal id_manual As Integer, ByVal nombre_archivo As String) As Boolean

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arencocina_registrar_archivo_manual", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Agregar parámetros con tipo y tamaño
                    cmd.Parameters.Add("@id_manual", SqlDbType.Int).Value = id_manual
                    cmd.Parameters.Add("@nombre_archivo", SqlDbType.VarChar, 150).Value = nombre_archivo

                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True
        Catch ex As Exception
            ' Aquí puedes registrar el error antes de relanzarlo
            ' LogError(ex.Message) 
            Throw ' Lanza el error para que sea manejado en niveles superiores
        End Try
    End Function

    Public Function ExisteTokenActivo(ByVal token As String) As DataSet
        Dim ds As New DataSet()

        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion_arsysusers").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arsuite_Validartoken", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Definir parámetro con tipo y tamaño
                    cmd.Parameters.Add("@token", SqlDbType.VarChar, 500).Value = token

                    ' Llenar el DataSet usando SqlDataAdapter
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(ds)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ' Registrar el error si es necesario
            ' LogError(ex.Message) 
            Throw ' Relanzar el error para ser manejado en un nivel superior
        End Try

        Return ds
    End Function

    Public Function CerrarSesionGlobal(ByVal tokenGlobal As String) As Boolean
        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion_arsysusers").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arsuite_CerrarSesionGlobal", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Agregar parámetros con tipo y tamaño
                    cmd.Parameters.Add("@token", SqlDbType.VarChar, 500).Value = tokenGlobal

                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True
        Catch ex As Exception
            ' Aquí puedes registrar el error antes de relanzarlo
            ' LogError(ex.Message) 
            Throw ' Lanza el error para que sea manejado en niveles superiores
        End Try
    End Function

    Public Function Registro_SesionSistema(ByVal tokenGlobal As String, ByVal usuario As String, ByVal CodigoSistema As String, ByVal PaginaAcceso As String) As Boolean
        Try
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("cadenaConexion_arsysusers").ConnectionString

            Using cn As New SqlConnection(strConnString)
                Using cmd As New SqlCommand("sp_arsuite_Registro_SesionSistema", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 0

                    ' Agregar parámetros con tipo y tamaño
                    cmd.Parameters.Add("@tokenGlobal", SqlDbType.VarChar, 500).Value = tokenGlobal
                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 50).Value = usuario
                    cmd.Parameters.Add("@CodigoSistema", SqlDbType.VarChar, 20).Value = CodigoSistema
                    cmd.Parameters.Add("@PaginaAcceso", SqlDbType.VarChar, 200).Value = PaginaAcceso

                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True
        Catch ex As Exception
            ' Aquí puedes registrar el error antes de relanzarlo
            ' LogError(ex.Message) 
            Throw ' Lanza el error para que sea manejado en niveles superiores
        End Try
    End Function

End Class
