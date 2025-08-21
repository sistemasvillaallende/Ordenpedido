using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace Web.Reportes
{
    public class OrdenPedidoPDF
    {
        public byte[] GenerarPDF(Entities.OrdenPedido oOrden, string logoPath = null, string usuario_imprime = null)
        {
            try
            {
                using (MemoryStream output = new MemoryStream())
                {
                    Document document = new Document(PageSize.A4, 40, 40, 40, 40);
                    PdfWriter.GetInstance(document, output);
                    document.Open();

                    // Fuentes
                    BaseFont bfHelvetica = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
                    BaseFont bfHelveticaBold = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false);

                    Font fontTitulo = new Font(bfHelveticaBold, 14, Font.BOLD);
                    Font fontSubtitulo = new Font(bfHelveticaBold, 9, Font.BOLD);
                    Font fontNormal = new Font(bfHelvetica, 9, Font.NORMAL);
                    Font fontPequeno = new Font(bfHelvetica, 8, Font.NORMAL);
                    Font fontTablaHeader = new Font(bfHelveticaBold, 8, Font.BOLD);

                    // HEADER MUNICIPALIDAD (arriba a la izquierda como en Crystal)
                    CrearHeaderMunicipalidad(document, oOrden, fontTitulo, fontSubtitulo, fontNormal, logoPath, usuario_imprime);

                    //// INFO PROVEEDOR, SOLICITUD
                    CrearInfoProveedorYSolicitud(document, oOrden, fontSubtitulo, fontNormal);

                    // DETALLE DE PRODUCTOS
                    CrearTablaProductos(document, oOrden, fontTablaHeader, fontPequeno);

                    // TOTAL
                    CrearTotal(document, oOrden, fontTablaHeader);

                    // OBSERVACIONES
                    CrearObservaciones(document, oOrden, fontSubtitulo, fontNormal);

                    // LINEA HORIZONTAL
                    CrearEncabezadoConLinea(document);

                    // NOTA FINAL y FIRMA
                    CrearNotaYFirmas(document, fontPequeno, fontNormal);

                    document.Close();
                    return output.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar PDF: " + ex.Message);
            }
        }

        private void CrearHeaderMunicipalidad(Document document, Entities.OrdenPedido oOrden, Font fontTitulo, Font fontSubtitulo,
          Font fontNormal, string logoPath = null, string usuario_imprime = null)
        {
            Paragraph salto = new Paragraph();
            salto.SpacingAfter = 1;
            // Imagen del logo
            Image logopng = Image.GetInstance(logoPath);
            logopng.ScaleAbsolute(80f, 80f); // Ajusta tamaño según necesidad

            // Tabla principal con 2 columnas: logo y datos
            PdfPTable headerTable = new PdfPTable(2)
            {
                WidthPercentage = 100,
                //HorizontalAlignment = Element.ALIGN_LEFT
            };
            headerTable.SetWidths(new float[] { 1f, 3f }); // Columna logo más chica

            // Celda del logo
            PdfPCell clLogo = new PdfPCell()
            {
                Image = logopng,
                Border = Rectangle.NO_BORDER,
                Padding = 10,
                //Border = Rectangle.NO_BORDER,
                //VerticalAlignment = Element.ALIGN_TOP,
                //PaddingBottom = 10
            };
            headerTable.AddCell(clLogo);

            // Celda de los datos de la municipalidad y título
            PdfPCell clDatos = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                PaddingBottom = 10,
                HorizontalAlignment = 1
            };

            // Título principal
            Paragraph titulo = new Paragraph("ORDEN DE PEDIDO", fontTitulo)
            {
                Alignment = Element.ALIGN_RIGHT
            };
            clDatos.AddElement(titulo);
            clDatos.AddElement(new Paragraph("\n"));

            //// Información de la orden
            Paragraph pNro_op = new Paragraph();
            pNro_op.Font = fontNormal;
            pNro_op.Alignment = Element.ALIGN_RIGHT;
            pNro_op.Add(string.Format("Nro: {0}", oOrden.nroOrden, fontNormal));
            clDatos.AddElement(pNro_op);
            //
            iTextSharp.text.Paragraph pfecha = new Paragraph();
            pfecha.Font = fontNormal;
            pfecha.Alignment = Element.ALIGN_RIGHT;
            pfecha.Add(string.Format("Fecha OP : {0}/{1}/{2}",
                oOrden.fechaOrden.Day.ToString("D2"),
                oOrden.fechaOrden.Month.ToString("D2"),
                oOrden.fechaOrden.Year.ToString(), fontNormal));
            clDatos.AddElement(pfecha);
            //
            //clDatos.AddElement(salto);
            iTextSharp.text.Paragraph phora = new Paragraph();
            phora.Font = fontNormal;
            phora.Alignment = Element.ALIGN_RIGHT;
            phora.Add(string.Format("Hora Impresión : {0}:{1}:{2}",
                oOrden.fechaOrden.Hour.ToString("D2"),
                oOrden.fechaOrden.Minute.ToString("D2"),
                oOrden.fechaOrden.Second.ToString("D2"), fontNormal));
            clDatos.AddElement(phora);

            Paragraph pUsuario_imprime = new Paragraph();
            pUsuario_imprime.Font = fontNormal;
            pUsuario_imprime.Alignment = Element.ALIGN_RIGHT;
            pUsuario_imprime.Add(string.Format("Usuario Imprime : {0}", usuario_imprime, fontNormal));
            clDatos.AddElement(pUsuario_imprime);

            headerTable.AddCell(clDatos);
            // Agregar tabla al documento
            document.Add(headerTable);
            // Espacio después de la cabecera
            document.Add(new Paragraph("\n"));
        }

        private void CrearRegionProveedorySolicitud(Document document, Entities.OrdenPedido oOrden, Font fontSubtitulo, Font fontNormal)
        {
            #region TABLA de Proveedor y Solicitud

            Paragraph salto = new Paragraph();
            salto.SpacingAfter = 1;
            PdfPTable tblTablaProveedorySolicitud = new PdfPTable(2)
            {
                WidthPercentage = 100,
                //Border = Rectangle.NO_BORDER,
                //Padding = 10,
            };
            PdfPCell clTablaProveedor = new PdfPCell()
            {
                BorderWidth = 0,
                BorderWidthTop = 1f,
                BorderWidthBottom = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                Padding = 10
            };


            PdfPCell clDatosProveedor = new PdfPCell()
            {
                BorderWidth = 0,
                BorderWidthTop = 1f,
                BorderWidthBottom = 1f,
                BorderWidthRight = 1f,
                BorderWidthLeft = 1f,
                Padding = 10
            };
            clDatosProveedor.AddElement(new Phrase("PROVEEDOR ", fontSubtitulo));
            clDatosProveedor.AddElement(salto);
            clDatosProveedor.AddElement(new Phrase(string.Format("Razon Social : {0}", oOrden.proveedor, fontNormal)));
            clDatosProveedor.AddElement(salto);
            clDatosProveedor.AddElement(new Phrase(string.Format("Nº Factura   : {0}", oOrden.nroFacturas, fontNormal)));
            clDatosProveedor.AddElement(salto);
            clDatosProveedor.AddElement(new Phrase(string.Format("Nº Presupuesto : {0}", oOrden.nroPresupuesto, fontNormal)));
            clDatosProveedor.AddElement(salto);
            clDatosProveedor.AddElement(new Phrase(string.Format("Forma Pago : {0}", oOrden.formaPago, fontNormal)));

            PdfPCell clDatosSolicitud = new PdfPCell()
            {
                BorderWidth = 0,
                BorderWidthTop = 1f,
                BorderWidthBottom = 1f,
                BorderWidthRight = 1f,
                Padding = 10,
                HorizontalAlignment = 1
            };

            Entities.Oficinas origen = BLL.OficinasBLL.getOficinaByPk(oOrden.codOficinaOrigen);
            clDatosSolicitud.AddElement(new Phrase("SOLICITUD ", fontSubtitulo));
            clDatosSolicitud.AddElement(salto);
            clDatosSolicitud.AddElement(new Phrase(string.Format("Destino  : {0}", oOrden.destino, fontNormal)));
            clDatosSolicitud.AddElement(salto);
            clDatosSolicitud.AddElement(new Phrase(string.Format("Origen   : {0}", origen.nombre, fontNormal)));
            clDatosSolicitud.AddElement(salto);
            clDatosSolicitud.AddElement(new Phrase(string.Format("Solicito : {0}", oOrden.solicitante, fontNormal)));
            clDatosSolicitud.AddElement(salto);
            clDatosSolicitud.AddElement(new Phrase(string.Format("Aprobo   : {0}", oOrden.aprobado, fontNormal)));


            tblTablaProveedorySolicitud.AddCell(clDatosProveedor);
            tblTablaProveedorySolicitud.AddCell(clDatosSolicitud);

            //clTablaVisado.AddElement(tblPases);
            //tblTablaVisado.AddCell(clTablaVisado);
            document.Add(tblTablaProveedorySolicitud);

            #endregion
        }

        private PdfPCell CrearCeldaInfo(string titulo, string valor, Font fontSubtitulo, Font fontNormal)
        {
            PdfPCell cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.AddElement(new Paragraph(titulo, fontSubtitulo));
            cell.AddElement(new Paragraph(valor, fontNormal));
            return cell;
        }

        //private void CrearInfoProveedorYSolicitudV2(Document document, Entities.OrdenPedido oOrden, Font fontSubtitulo, Font fontNormal)
        //{
        //    // 🔹 Tabla principal de 2 columnas
        //    PdfPTable tablaPrincipal = new PdfPTable(2);
        //    tablaPrincipal.WidthPercentage = 100;
        //    tablaPrincipal.SetWidths(new float[] { 1f, 1f });

        //    // ================== COLUMNA PROVEEDOR ==================
        //    PdfPTable tablaProveedor = new PdfPTable(2);
        //    tablaProveedor.WidthPercentage = 100;
        //    tablaProveedor.SetWidths(new float[] { 1f, 2f });

        //    // Encabezado PROVEEDOR
        //    PdfPCell headerProveedor = new PdfPCell(new Phrase("PROVEEDOR", fontSubtitulo));
        //    headerProveedor.Colspan = 2;
        //    headerProveedor.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    headerProveedor.HorizontalAlignment = Element.ALIGN_CENTER;
        //    headerProveedor.Padding = 5;
        //    tablaProveedor.AddCell(headerProveedor);

        //    // Datos proveedor
        //    tablaProveedor.AddCell(new PdfPCell(new Phrase("Razón social:", fontNormal)) { Border = Rectangle.NO_BORDER });
        //    tablaProveedor.AddCell(new PdfPCell(new Phrase(oOrden.proveedor ?? "", fontNormal)) { Border = Rectangle.NO_BORDER });

        //    tablaProveedor.AddCell(new PdfPCell(new Phrase("Nº Factura:", fontNormal)) { Border = Rectangle.NO_BORDER });
        //    tablaProveedor.AddCell(new PdfPCell(new Phrase(oOrden.nroFacturas ?? "", fontNormal)) { Border = Rectangle.NO_BORDER });

        //    tablaProveedor.AddCell(new PdfPCell(new Phrase("Nº Presupuesto:", fontNormal)) { Border = Rectangle.NO_BORDER });
        //    tablaProveedor.AddCell(new PdfPCell(new Phrase(oOrden.nroPresupuesto ?? "", fontNormal)) { Border = Rectangle.NO_BORDER });

        //    tablaProveedor.AddCell(new PdfPCell(new Phrase("Forma de Pago:", fontNormal)) { Border = Rectangle.NO_BORDER });
        //    tablaProveedor.AddCell(new PdfPCell(new Phrase(oOrden.formaPago ?? "", fontNormal)) { Border = Rectangle.NO_BORDER });


        //    // ================== COLUMNA SOLICITUD ==================
        //    PdfPTable tablaSolicitud = new PdfPTable(2);
        //    tablaSolicitud.WidthPercentage = 100;
        //    tablaSolicitud.SetWidths(new float[] { 1f, 2f });

        //    // Encabezado SOLICITUD
        //    PdfPCell headerSolicitud = new PdfPCell(new Phrase("SOLICITUD", fontSubtitulo));
        //    headerSolicitud.Colspan = 2;
        //    headerSolicitud.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    headerSolicitud.HorizontalAlignment = Element.ALIGN_CENTER;
        //    headerSolicitud.Padding = 5;
        //    tablaSolicitud.AddCell(headerSolicitud);

        //    // Datos solicitud
        //    tablaSolicitud.AddCell(new PdfPCell(new Phrase("Destino:", fontNormal)) { Border = Rectangle.NO_BORDER });
        //    tablaSolicitud.AddCell(new PdfPCell(new Phrase(oOrden.destino ?? "", fontNormal)) { Border = Rectangle.NO_BORDER });

        //    tablaSolicitud.AddCell(new PdfPCell(new Phrase("Origen:", fontNormal)) { Border = Rectangle.NO_BORDER });
        //    //tablaSolicitud.AddCell(new PdfPCell(new Phrase(oOrden.origen ?? "", fontNormal)) { Border = Rectangle.NO_BORDER });

        //    tablaSolicitud.AddCell(new PdfPCell(new Phrase("Solicitó:", fontNormal)) { Border = Rectangle.NO_BORDER });
        //    tablaSolicitud.AddCell(new PdfPCell(new Phrase(oOrden.solicitante ?? "", fontNormal)) { Border = Rectangle.NO_BORDER });

        //    tablaSolicitud.AddCell(new PdfPCell(new Phrase("Aprobó:", fontNormal)) { Border = Rectangle.NO_BORDER });
        //    tablaSolicitud.AddCell(new PdfPCell(new Phrase(oOrden.aprobado ?? "", fontNormal)) { Border = Rectangle.NO_BORDER });


        //    // 🔹 Insertar las subtablas en la tabla principal
        //    PdfPCell celdaProveedor = new PdfPCell(tablaProveedor);
        //    PdfPCell celdaSolicitud = new PdfPCell(tablaSolicitud);

        //    tablaPrincipal.AddCell(celdaProveedor);
        //    tablaPrincipal.AddCell(celdaSolicitud);

        //    tablaPrincipal.SpacingAfter = 10;
        //    document.Add(tablaPrincipal);
        //}

        private void CrearInfoProveedorYSolicitud(Document document, Entities.OrdenPedido oOrden, Font fontSubtitulo, Font fontNormal)
        {
            // ✅ Tabla contenedora principal con borde
            PdfPTable tablaWrapper = new PdfPTable(1);
            tablaWrapper.WidthPercentage = 100;

            // 🔹 Tabla interna de 2 columnas (sin borde propio)
            PdfPTable tablaPrincipal = new PdfPTable(2);
            tablaPrincipal.WidthPercentage = 100;
            tablaPrincipal.SetWidths(new float[] { 1f, 1f });

            // ================== TABLA PROVEEDOR ==================
            PdfPTable tablaProveedor = new PdfPTable(1);
            tablaProveedor.WidthPercentage = 100;

            // Header PROVEEDOR con fondo gris
            PdfPCell headerProveedor = new PdfPCell(new Phrase("PROVEEDOR", fontSubtitulo))
            {
                BackgroundColor = BaseColor.LIGHT_GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5,
                Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidth = 1f,
                BorderColor = BaseColor.BLACK
            };
            tablaProveedor.AddCell(headerProveedor);

            // Filas con datos
            tablaProveedor.AddCell(CrearCeldaDato("Razón social:", oOrden.proveedor ?? "", fontSubtitulo, fontNormal));
            tablaProveedor.AddCell(CrearCeldaDato("Nº Factura:", oOrden.nroFacturas ?? "", fontSubtitulo, fontNormal));
            tablaProveedor.AddCell(CrearCeldaDato("Nº Presupuesto:", oOrden.nroPresupuesto ?? "", fontSubtitulo, fontNormal));
            tablaProveedor.AddCell(CrearCeldaDato("Forma de Pago:", oOrden.formaPago ?? "", fontSubtitulo, fontNormal));

            // ================== TABLA SOLICITUD ==================
            PdfPTable tablaSolicitud = new PdfPTable(1);
            tablaSolicitud.WidthPercentage = 100;

            // Header SOLICITUD con fondo gris
            PdfPCell headerSolicitud = new PdfPCell(new Phrase("SOLICITUD", fontSubtitulo))
            {
                BackgroundColor = BaseColor.LIGHT_GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5,
                Border = Rectangle.BOTTOM_BORDER,
                BorderWidth = 1f,
                BorderColor = BaseColor.BLACK
            };
            tablaSolicitud.AddCell(headerSolicitud);

            // Filas con datos
            Entities.Oficinas origen = BLL.OficinasBLL.getOficinaByPk(oOrden.codOficinaOrigen);
            tablaSolicitud.AddCell(CrearCeldaDato("Destino:", origen.nombre ?? "", fontSubtitulo, fontNormal));
            tablaSolicitud.AddCell(CrearCeldaDato("Origen:", oOrden.destino ?? "", fontSubtitulo, fontNormal));
            tablaSolicitud.AddCell(CrearCeldaDato("Solicitó:", oOrden.solicitante ?? "", fontSubtitulo, fontNormal));
            tablaSolicitud.AddCell(CrearCeldaDato("Aprobó:", oOrden.aprobado ?? "", fontSubtitulo, fontNormal));

            // 🔹 Insertar subtablas en la tabla principal
            PdfPCell celdaProveedor = new PdfPCell(tablaProveedor)
            {
                Border = Rectangle.RIGHT_BORDER,
                BorderWidth = 1f,
                BorderColor = BaseColor.BLACK,
                Padding = 0
            };
            PdfPCell celdaSolicitud = new PdfPCell(tablaSolicitud)
            {
                Border = Rectangle.NO_BORDER,
                Padding = 0
            };

            tablaPrincipal.AddCell(celdaProveedor);
            tablaPrincipal.AddCell(celdaSolicitud);

            // ✅ Envolver TODO en una celda con borde negro
            PdfPCell bloqueConBorde = new PdfPCell(tablaPrincipal)
            {
                Border = Rectangle.BOX,
                BorderWidth = 1f,
                BorderColor = BaseColor.BLACK,
                Padding = 0  // Sin padding para que el header toque el borde
            };

            tablaWrapper.AddCell(bloqueConBorde);
            tablaWrapper.SpacingAfter = 10;
            document.Add(tablaWrapper);
        }

        private PdfPCell CrearCeldaDato(string titulo, string valor, Font fontTitulo, Font fontValor)
        {
            // Celda izquierda (Título)
            PdfPCell celdaTitulo = new PdfPCell(new Phrase(titulo, fontTitulo))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 2
            };

            // Celda derecha (Valor)
            PdfPCell celdaValor = new PdfPCell(new Phrase(valor ?? "", fontValor))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 2
            };

            // Subtabla de 2 columnas (clave : valor)
            PdfPTable fila = new PdfPTable(2);
            fila.WidthPercentage = 100;
            fila.SetWidths(new float[] { 1f, 2f });
            fila.AddCell(celdaTitulo);
            fila.AddCell(celdaValor);

            // Empaquetamos esa fila como una sola celda para insertarla en la tabla principal
            PdfPCell filaComoCelda = new PdfPCell(fila)
            {
                Border = Rectangle.NO_BORDER,
                Padding = 0
            };

            return filaComoCelda;
        }

        //private void CrearInfoProveedor(Document document, Entities.OrdenPedido oOrden, Font fontSubtitulo, Font fontNormal)
        //{
        //    Paragraph titulo = new Paragraph("PROVEEDOR", fontSubtitulo);
        //    titulo.SpacingBefore = 5;
        //    document.Add(titulo);

        //    PdfPTable tabla = new PdfPTable(2);
        //    tabla.WidthPercentage = 100;

        //    tabla.AddCell(CrearCeldaInfo("Razón social:", oOrden.proveedor ?? "", fontSubtitulo, fontNormal));
        //    tabla.AddCell(CrearCeldaInfo("Nº Factura:", oOrden.nroFacturas ?? "", fontSubtitulo, fontNormal));
        //    tabla.AddCell(CrearCeldaInfo("Nº Presupuesto:", oOrden.nroPresupuesto ?? "", fontSubtitulo, fontNormal));
        //    tabla.AddCell(CrearCeldaInfo("Forma de Pago:", oOrden.formaPago ?? "", fontSubtitulo, fontNormal));

        //    tabla.SpacingAfter = 10;
        //    document.Add(tabla);


        //}

        //private void CrearInfoSolicitud(Document document, Entities.OrdenPedido oOrden, Font fontSubtitulo, Font fontNormal)
        //{
        //    Paragraph titulo = new Paragraph("SOLICITUD", fontSubtitulo);
        //    titulo.SpacingBefore = 5;
        //    document.Add(titulo);

        //    PdfPTable tabla = new PdfPTable(2);
        //    tabla.WidthPercentage = 100;

        //    tabla.AddCell(CrearCeldaInfo("Destino:", oOrden.destino ?? "", fontSubtitulo, fontNormal));
        //    tabla.AddCell(CrearCeldaInfo("Origen:", "", fontSubtitulo, fontNormal));
        //    tabla.AddCell(CrearCeldaInfo("Solicitó:", oOrden.solicitante ?? "", fontSubtitulo, fontNormal));
        //    tabla.AddCell(CrearCeldaInfo("Aprobó:", oOrden.aprobado ?? "", fontSubtitulo, fontNormal));

        //    tabla.SpacingAfter = 10;
        //    document.Add(tabla);
        //}

        //private void CrearTablaProductos(Document document, Entities.OrdenPedido oOrden, Font fontTablaHeader, Font fontPequeno)
        //{
        //    PdfPTable tabla = new PdfPTable(4);
        //    tabla.WidthPercentage = 100;
        //    tabla.SetWidths(new float[] { 4.5f, 0.8f, 1.5f, 1.5f });

        //    // Encabezados principales
        //    string[] headers = { "Descripcion", "Cant.", "P. Unitario", "Subtotal" };
        //    foreach (var h in headers)
        //    {
        //        PdfPCell cell = new PdfPCell(new Phrase(h, fontTablaHeader));
        //        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //        cell.BackgroundColor = new BaseColor(220, 220, 220);
        //        tabla.AddCell(cell);
        //    }

        //    // Encabezado anexo/imputación
        //    PdfPCell anexo = new PdfPCell(new Phrase("Anexo", fontTablaHeader));
        //    anexo.BackgroundColor = new BaseColor(220, 220, 220);
        //    tabla.AddCell(anexo);

        //    PdfPCell imputacion = new PdfPCell(new Phrase("IMPUTACION", fontTablaHeader));
        //    imputacion.Colspan = 3;
        //    imputacion.HorizontalAlignment = Element.ALIGN_CENTER;
        //    imputacion.BackgroundColor = new BaseColor(220, 220, 220);
        //    tabla.AddCell(imputacion);

        //    // Sub-encabezados de imputación
        //    tabla.AddCell(new PdfPCell(new Phrase("", fontPequeno))); // vacío bajo "Anexo"
        //    tabla.AddCell(new PdfPCell(new Phrase("Inciso P. Ppal. Item", fontPequeno)));
        //    tabla.AddCell(new PdfPCell(new Phrase("Sub Item.", fontPequeno)));
        //    tabla.AddCell(new PdfPCell(new Phrase("Part. S. Part", fontPequeno)));

        //    // Detalles
        //    if (oOrden.detalle != null)
        //    {
        //        foreach (var d in oOrden.detalle)
        //        {
        //            tabla.AddCell(new Phrase(d.descItems ?? "", fontPequeno));
        //            tabla.AddCell(new Phrase(d.cant.ToString("0.00"), fontPequeno));
        //            tabla.AddCell(new Phrase("$ " + d.precio.ToString("N2"), fontPequeno));
        //            tabla.AddCell(new Phrase("$ " + d.importe.ToString("N2"), fontPequeno));

        //            // Fila imputación (vacía)
        //            tabla.AddCell(new Phrase("", fontPequeno));
        //            tabla.AddCell(new Phrase("", fontPequeno));
        //            tabla.AddCell(new Phrase("", fontPequeno));
        //            tabla.AddCell(new Phrase("", fontPequeno));
        //        }
        //    }

        //    document.Add(tabla);
        //}


        private void CrearTablaProductos(Document document, Entities.OrdenPedido oOrden, Font fontTablaHeader, Font fontPequeno)
        {
            // Tabla con 9 columnas según la imagen
            PdfPTable tabla = new PdfPTable(11);
            tabla.WidthPercentage = 100;
            // Ajustar anchos según la imagen: Anexo, Inciso, P.Ppal, Item, Sub Item, Part., S.Part, Descripcion(más ancha), Cant., P.Unitario, Subtotal
            //tabla.SetWidths(new float[] { 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 3.5f, 0.8f, 1.2f, 1.2f });
            tabla.SetWidths(new float[] { 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 3.5f, 0.8f, 1.6f, 1.6f });


            // ============ PRIMERA FILA: HEADER "IMPUTACION" ============
            PdfPCell headerImputacion = new PdfPCell(new Phrase("IMPUTACION", fontTablaHeader))
            {
                Colspan = 11, // Abarca todas las columnas
                HorizontalAlignment = Element.ALIGN_CENTER,
                BackgroundColor = new BaseColor(220, 220, 220),
                BorderWidth = 1f,
                BorderColor = BaseColor.BLACK,
                Padding = 5
            };
            tabla.AddCell(headerImputacion);

            // ============ SEGUNDA FILA: SUB-HEADERS ============
            string[] subHeaders = { "Anexo", "Inciso", "P. Ppal.", "Item", "Sub Item.", "Part.", "S. Part", "Descripción", "Cant.", "P. Unitario", "Subtotal" };

            foreach (var header in subHeaders)
            {
                PdfPCell cell = new PdfPCell(new Phrase(header, fontPequeno))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(220, 220, 220),
                    BorderWidth = 1f,
                    BorderColor = BaseColor.BLACK,
                    Padding = 3
                };
                tabla.AddCell(cell);
            }

            // ============ FILAS DE DATOS ============
            if (oOrden.detalle != null)
            {
                foreach (var d in oOrden.detalle)
                {
                    // Columnas de imputación (vacías por ahora - aquí puedes agregar los datos de imputación)
                    tabla.AddCell(CrearCeldaDato("", fontPequeno)); // Anexo
                    tabla.AddCell(CrearCeldaDato("", fontPequeno)); // Inciso
                    tabla.AddCell(CrearCeldaDato("", fontPequeno)); // P. Ppal.
                    tabla.AddCell(CrearCeldaDato("", fontPequeno)); // Item
                    tabla.AddCell(CrearCeldaDato("", fontPequeno)); // Sub Item.
                    tabla.AddCell(CrearCeldaDato("", fontPequeno)); // Part.
                    tabla.AddCell(CrearCeldaDato("", fontPequeno)); // S. Part

                    // Columnas de producto
                    tabla.AddCell(CrearCeldaDato(d.descItems ?? "", fontPequeno)); // Descripción
                    tabla.AddCell(CrearCeldaDatoNumerico(d.cant.ToString("0.00"), fontPequeno)); // Cantidad
                    tabla.AddCell(CrearCeldaDatoNumerico("$ " + d.precio.ToString("N2"), fontPequeno)); // P. Unitario
                    tabla.AddCell(CrearCeldaDatoNumerico("$ " + d.importe.ToString("N2"), fontPequeno)); // Subtotal
                }
            }

            // ============ FILA TOTAL ============
            // Celdas vacías para las columnas de imputación y descripción
            for (int i = 0; i < 8; i++)
            {
                tabla.AddCell(CrearCeldaDato("", fontPequeno));
            }

            // Celda "Total:"
            PdfPCell celdaTotal = new PdfPCell(new Phrase("Total:", fontTablaHeader))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                BorderWidth = 1f,
                BorderColor = BaseColor.BLACK,
                Padding = 3,
                BackgroundColor = BaseColor.WHITE
            };
            tabla.AddCell(celdaTotal);

            // Celda vacía
            tabla.AddCell(CrearCeldaDato("", fontPequeno));

            // Celda con el monto total - Calculado manualmente sin LINQ
            decimal total = 0;
            if (oOrden.detalle != null)
            {
                foreach (var d in oOrden.detalle)
                {
                    total += d.importe;
                }
            }

            PdfPCell celdaMontoTotal = new PdfPCell(new Phrase("$ " + total.ToString("N2"), fontTablaHeader))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                BorderWidth = 1f,
                BorderColor = BaseColor.BLACK,
                Padding = 3,
                BackgroundColor = BaseColor.WHITE
            };
            tabla.AddCell(celdaMontoTotal);

            tabla.SpacingAfter = 10;
            document.Add(tabla);
        }

        // Método auxiliar para crear celdas de dato con formato estándar
        private PdfPCell CrearCeldaDato(string texto, Font font)
        {
            return new PdfPCell(new Phrase(texto, font))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                BorderWidth = 1f,
                BorderColor = BaseColor.BLACK,
                Padding = 3
            };
        }

        // Método auxiliar para crear celdas de datos numéricos (alineados a la derecha)
        private PdfPCell CrearCeldaDatoNumerico(string texto, Font font)
        {
            return new PdfPCell(new Phrase(texto, font))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                BorderWidth = 1f,
                BorderColor = BaseColor.BLACK,
                Padding = 3
            };
        }

        //
        //private void CrearTotal(Document document, Entities.OrdenPedido oOrden, Font fontTablaHeader)
        //{
        //    Paragraph total = new Paragraph($"Total: $ {oOrden.total.ToString("N2")}", fontTablaHeader);
        //    total.Alignment = Element.ALIGN_RIGHT;
        //    total.SpacingBefore = 5;
        //    total.SpacingAfter = 10;
        //    document.Add(total);
        //}
        private void CrearTotal(Document document, Entities.OrdenPedido oOrden, Font fontTablaHeader)
        {
            // Crear tabla de una columna
            PdfPTable tablaTotal = new PdfPTable(1);
            tablaTotal.WidthPercentage = 100;
            tablaTotal.SpacingBefore = 5f;
            tablaTotal.SpacingAfter = 10f;

            // Crear celda con el total
            Phrase contenido = new Phrase($"Total: $ {oOrden.total.ToString("N2")}", fontTablaHeader);
            PdfPCell celdaTotal = new PdfPCell(contenido);
            celdaTotal.HorizontalAlignment = Element.ALIGN_RIGHT;
            celdaTotal.BackgroundColor = new BaseColor(230, 230, 230); // Gris claro
            celdaTotal.Border = Rectangle.BOX;
            celdaTotal.Padding = 5f;

            tablaTotal.AddCell(celdaTotal);
            document.Add(tablaTotal);
        }

        //
        private void CrearObservaciones(Document document, Entities.OrdenPedido oOrden, Font fontSubtitulo, Font fontNormal)
        {
            Paragraph titulo = new Paragraph("OBSERVACIONES", fontSubtitulo);
            titulo.SpacingBefore = 5;
            document.Add(titulo);

            Paragraph obs = new Paragraph(oOrden.obs ?? "", fontNormal);
            obs.SpacingAfter = 15;
            document.Add(obs);
        }

        //
        private void CrearNotaYFirmas(Document document, Font fontPequeno, Font fontNormal)
        {
            // Crear tabla con dos columnas
            PdfPTable tabla = new PdfPTable(2);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(new float[] { 3f, 1f }); // Más espacio para la nota

            // Celda de la nota
            string textoNota = "Factura/s NO constatadas por ser orden con contrato, presupuesto u\n" +
                               "otro documento válido – se deberá cargar la Factura en la Orden de";
            PdfPCell celdaNota = new PdfPCell(new Phrase(textoNota, fontPequeno));
            celdaNota.Border = Rectangle.NO_BORDER;
            celdaNota.HorizontalAlignment = Element.ALIGN_LEFT;
            celdaNota.VerticalAlignment = Element.ALIGN_MIDDLE;
            celdaNota.PaddingRight = 10f;
            tabla.AddCell(celdaNota);

            // Celda de firmas combinadas
            Paragraph firmas = new Paragraph();
            firmas.Alignment = Element.ALIGN_CENTER;
            firmas.Add(new Phrase("Vº Bº\n", fontNormal));
            firmas.Add(new Phrase("Contaduría", fontNormal));

            PdfPCell celdaFirmas = new PdfPCell(firmas);
            celdaFirmas.Border = Rectangle.BOX;
            celdaFirmas.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaFirmas.VerticalAlignment = Element.ALIGN_MIDDLE;
            celdaFirmas.MinimumHeight = 60f;
            tabla.AddCell(celdaFirmas);

            // Agregar tabla al documento
            tabla.SpacingBefore = 10f;
            document.Add(tabla);
        }

        //
        private void CrearEncabezadoConLinea(Document document)
        {
            // Línea horizontal
            LineSeparator linea = new LineSeparator();
            linea.Offset = -2f; // Ajusta la posición vertical si lo necesitás
            linea.LineWidth = 1f; // Grosor de la línea
            linea.LineColor = BaseColor.BLACK;

            document.Add(new Chunk(linea));
        }

    }
}