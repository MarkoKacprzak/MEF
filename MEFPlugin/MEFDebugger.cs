using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace MEFPlugin
{
    public class MefDebugger
    {
        private const string DbgMefCatalog = "MEF: Found catalog: {0}";
        private const string DbgMefKey = "      With key: {0} = {1}";
        private const string DbgMefPart = "MEF: Found part: {0}";
        private const string DbgMefWithExport = "   With export:";
        private const string DbgMefWithImport = "   With import: {0}";
        private const string DbgMefWithKey = "   With key: {0} = {1}";
        private readonly CompositionContainer _container;

        public MefDebugger(CompositionContainer container)
        {
            _container = container;
            _container.ExportsChanged += ExportsChanged;
            DebugCatalog((AggregateCatalog)container.Catalog);
        }

        private void ExportsChanged(object sender, ExportsChangeEventArgs arg)
        {
            try
            {
                if (arg.AddedExports != null)
                {
                    ParseExport($"** Added Export: ", arg.AddedExports);
                }

                if (arg.RemovedExports != null)
                {
                    ParseExport($"** Removed Export: ", arg.RemovedExports);
                }

                if (arg.ChangedContractNames != null)
                {
                    var first = true;
                    foreach (var contract in arg.ChangedContractNames)
                    {
                        if (first)
                        {
                            Console.WriteLine($"=== Contracts Changed ===");
                            first = false;
                        }
                        Console.WriteLine($" ===>{contract}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"### MEF Debugger Error {e.Message}");
            }
        }

        private static void ParseExport(string tag, IEnumerable<ExportDefinition> exports)
        {
            foreach (var exportDefinition in exports)
            {
                Console.WriteLine($"{tag} => Exoirt {exportDefinition}");
                if (exportDefinition.Metadata == null)
                    continue;
                foreach (var key in exportDefinition.Metadata.Keys)
                {
                    Console.WriteLine($"{tag} ..... => Key: {key}: {exportDefinition.Metadata[key]}");
                }
            }
        }
        private static void LogInfo(string format, params object[] args)
        {
            Console.WriteLine("Info: " + format, args);
        }
        /// <summary>
        ///     Debug the catalog
        /// </summary>
        /// <param name="srcCatalog">The source catalog</param>
        private void DebugCatalog(AggregateCatalog srcCatalog)
        {
            foreach (var catalog in srcCatalog.Catalogs)
            {
                LogInfo(DbgMefCatalog, catalog);

                foreach (var part in catalog.Parts)
                {
                    LogInfo(DbgMefPart, part);

                    if (part.Metadata != null)
                    {
                        foreach (string key in part.Metadata.Keys)
                        {
                            LogInfo(DbgMefWithKey, key, part.Metadata[key]);
                        }
                    }

                    foreach (var import in part.ImportDefinitions)
                    {
                        LogInfo(DbgMefWithImport, import);
                    }

                    ParseExport(DbgMefWithExport, part.ExportDefinitions);

                    foreach (var export in part.ExportDefinitions)
                    {
                        LogInfo("{0} {1}", DbgMefWithExport, export);

                        if (export.Metadata == null)
                        {
                            continue;
                        }

                        foreach (string key in export.Metadata.Keys)
                        {
                            LogInfo(DbgMefKey, key, export.Metadata[key]);
                        }
                    }
                }
            }
        }
        public void Close()
        {
            Console.WriteLine($"Done debugging.");
            _container.ExportsChanged -= ExportsChanged;
        }
    }
}