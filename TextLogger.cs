public class TextLogger : ITextLogger
    {
        private IConfiguration config;

        public TextLogger(IConfiguration configuration)
        {
            this.config = configuration;
        }

        public void writeLog(string message)
        {
            string ruta = System.Environment.CurrentDirectory + @"\logs";

            if (config["HabilitarLogTxt"] != null)
            {
                if (Convert.ToBoolean(config["HabilitarLogTxt"].ToString()))
                {
                    string fechahoy = DateTime.Now.ToString("yyyyMMdd");
                    string rutaLog = ruta + @"\" + "log" + fechahoy + ".txt";

                    if (!System.IO.Directory.Exists(ruta))
                        System.IO.Directory.CreateDirectory(ruta);

                    try
                    {
                        using (System.IO.StreamWriter sw = (System.IO.File.Exists(rutaLog)) ? System.IO.File.AppendText(rutaLog) : System.IO.File.CreateText(rutaLog))
                        {
                            try
                            {
                                message = DateTime.Now.ToString() + ":: " + message;
                                sw.WriteLine(message);
                            }
                            catch (FileNotFoundException ex)
                            {
                                System.Diagnostics.Debug.WriteLine("ERROR***" + DateTime.Now.ToString() + "--" + "Message: " + ex.Message + Environment.NewLine + "StackTrace: " + ex.StackTrace);
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine("ERROR***" + DateTime.Now.ToString() + "--" + "Message: " + ex.Message + Environment.NewLine + "InnerException: " + ex.InnerException + Environment.NewLine + "StackTrace: " + ex.StackTrace);
                            }
                            finally
                            {
                                sw.Close();
                            }
                        }
                    }
                    catch (FormatException ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error {ex.GetType().Name} en writeLog: {ex.Message} / {ex.StackTrace}");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error {ex.GetType().Name} en writeLog: {ex.Message} / {ex.StackTrace}");
                    }
                }
            }
        }
    }