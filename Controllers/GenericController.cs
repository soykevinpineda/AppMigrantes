using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Migrantes.Data;
using Migrantes.Models.DTO;
using Migrantes.ViewModels;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using cm = System.ComponentModel;


namespace Migrantes.Controllers
{
    public class GenericController : Controller
    {
       
        public static List<PersonaDTO> oPersonasExcel;


        public FileResult ExportarPersonasExcel(string[] nombrePropiedades)
        {
            byte[] buffer = ExportarExcelGeneric(nombrePropiedades, oPersonasExcel);
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }



        public string Error = "Ha ocurrido un evento inesperado, fuera de control de esta aplicación";


        public byte[] ExportarExcelGeneric<T>(string[] nombrePropiedades, List<T> oListaDataGeneric)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                try
                {
                    using (ExcelPackage ep = new ExcelPackage())
                    {
                        ep.Workbook.Worksheets.Add("DATA");

                        ExcelWorksheet ws = ep.Workbook.Worksheets[0];

                        Dictionary<string, string> diccionario = cm.TypeDescriptor.GetProperties(typeof(T))
                            .Cast<cm.PropertyDescriptor>().ToDictionary(p => p.Name, p => p.DisplayName);

                        Color colorNombreColumns = System.Drawing.ColorTranslator.FromHtml("#000000");
                        //Color colorFilas = System.Drawing.ColorTranslator.FromHtml("#CCCCFF");

                        if (nombrePropiedades != null && nombrePropiedades.Length > 0)//Validar checks nulos
                        {
                            if (oListaDataGeneric.Count < 1)
                            {
                                for (int i = 0; i < nombrePropiedades.Length; i++)
                                {
                                    ws.Cells[1, i + 1].Value = diccionario[nombrePropiedades[i]];//Para llenar las nombres de las columnas
                                    ws.Cells[ws.Dimension.Address].Style.Font.Bold = true;
                                    ws.Cells[ws.Dimension.Address].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    ws.Cells[ws.Dimension.Address].Style.Fill.BackgroundColor.SetColor(colorNombreColumns);
                                    ws.Cells[ws.Dimension.Address].Style.Font.Color.SetColor(Color.White);
                                    ws.Cells[ws.Dimension.Address].AutoFitColumns();
                                    ws.Cells[ws.Dimension.Address].AutoFilter = true;
                                }
                            }
                            else
                            {
                                //For para llenar nombres de columnas
                                for (int i = 0; i < nombrePropiedades.Length; i++)
                                {
                                    ws.View.FreezePanes(2, 1);
                                    ws.Cells[1, i + 1].Style.Font.Bold = true;
                                    ws.Cells[1, i + 1].Value = diccionario[nombrePropiedades[i]];//Para llenar las nombres de las columnas  
                                    ws.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                                    ws.View.ShowGridLines = false;
                                }

                                //Para empezar a pintar la data
                                int fila = 2;
                                int col = 1;

                                //item se convierte en una fila, porque es un objeto, un objeto de la lista (ELEMENTO, se pinta en una fila)
                                foreach (object item in oListaDataGeneric)
                                {
                                    col = 1;

                                    foreach (string propiedad in nombrePropiedades)
                                    {
                                        ws.Cells[fila, col].Value =
                                          item.GetType().GetProperty(propiedad).GetValue(item).ToString();
                                        ws.Cells[fila, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                                        col++;
                                    }

                                    fila++;
                                }

                                int filaFin = fila - 1;
                                int colFin = col - 1;

                                ExcelRange rg = ws.Cells[1, 1, filaFin, colFin];

                                string tableName = "Tabla";

                                ExcelTable table = ws.Tables.Add(rg, tableName);

                                table.TableStyle = TableStyles.Dark8;

                                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            }
                        }

                        ep.SaveAs(ms);
                        byte[] buffer = ms.ToArray();
                        return buffer;
                    }
                }
                catch (Exception e)
                {
                    Error = e.Message;
                    throw;
                }
            }
        }


    }

}
