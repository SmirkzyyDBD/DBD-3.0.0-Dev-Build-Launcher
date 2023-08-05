using System;
using System.Diagnostics;

namespace DevBuildLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Dev Build Launcher - Smirkzyy";
            PrintLogo();
            string? serverPath = GetServerPath();
            StartServer(serverPath);
            PrintLogo();
            string? gamePath = GetGamePath();
            MainMenu(gamePath);
        }

        static void PrintLogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"    _____                ____        _ _     _    _                            _               ");
            Console.WriteLine(@"   |  __ \              |  _ \      (_) |   | |  | |                          | |              ");
            Console.WriteLine(@"   | |  | | _____   __  | |_) |_   _ _| | __| |  | |     __ _ _   _ _ __   ___| |__   ___ _ __ ");
            Console.WriteLine(@"   | |  | |/ _ \ \ / /  |  _ <| | | | | |/ _\`|  | |    / _\`| | | | '_ \ / __| '_ \ / _ \ '__|");
            Console.WriteLine(@"   | |__| |  __/\ V /   | |_) | |_| | | | (_| |  | |___| (_| | |_| | | | | (__| | | |  __/ |   ");
            Console.WriteLine(@"   |_____/ \___| \_/    |____/ \__,_|_|_|\__,_|  |______\__,_|\__,_|_| |_|\___|_| |_|\___|_|   ");
            Console.WriteLine(@"                                          Made by Smirkzyy                    ");
            Console.WriteLine(@" ------------------------------------------------------------------------------------------------");
            Console.ResetColor();
        }

        static string? GetServerPath()
        {
            string? serverPath = null;
            while (string.IsNullOrEmpty(serverPath))
            {
                Console.WriteLine();
                if (System.IO.File.Exists("serverpath.txt"))
                {
                    serverPath = System.IO.File.ReadAllText("serverpath.txt").Trim();
                }
                else
                {
                    enterpath:
                    Console.WriteLine();
                    Console.Write("Enter the path where your Dev Build Server is located: ");
                    serverPath = Console.ReadLine().Trim();
                    if (!serverPath.EndsWith("\\"))
                    {
                        serverPath += "\\";
                    }
                    if (!File.Exists(serverPath + "run.bat"))
                    {
                        Console.WriteLine("Server Run file doesnt exist!");
                        goto enterpath;
                    }

                }

                if (!serverPath.EndsWith("\\"))
                    serverPath += "\\";
                
                if (!System.IO.Directory.Exists(serverPath))
                {
                    Console.WriteLine(serverPath);
                    Console.WriteLine("The Dev Build server cannot be found.");
                    serverPath = null;
                }
                else
                {
                    System.IO.File.WriteAllText("serverpath.txt", serverPath);
                }
            }
            return serverPath;
        }

        static void StartServer(string? serverPath)
        {
            Console.WriteLine("Starting Server!");
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                WorkingDirectory = serverPath,
                Arguments = "/c npm run start",
                CreateNoWindow = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(psi);
            System.Threading.Thread.Sleep(5000); // Wait for the server to start
        }

        static string? GetGamePath()
        {
            string? gamePath = null;
            while (string.IsNullOrEmpty(gamePath))
            {
                Console.WriteLine();
                if (System.IO.File.Exists("gamepath.txt"))
                {
                    gamePath = System.IO.File.ReadAllText("gamepath.txt").Trim();
                }
                else
                {
                    Console.Write("Enter the path where \"DeadByDaylight.exe\" is located: ");
                    gamePath = Console.ReadLine().Trim();
                }

                if (!gamePath.EndsWith("\\"))
                    gamePath += "\\";

                if (!System.IO.File.Exists(gamePath + "DeadByDaylight.exe"))
                {
                    Console.WriteLine(gamePath);
                    Console.WriteLine("\"DeadByDaylight.exe\" cannot be found.");
                    gamePath = null;
                }
                else
                {
                    System.IO.File.WriteAllText("gamepath.txt", gamePath);
                }
            }
            return gamePath;
        }

        static void MainMenu(string? gamePath)
        {
            while (true)
            {
                PrintLogo();
                Console.WriteLine("[1] Launch Dev Build");
                Console.WriteLine("[2] Launch Dev Build - No Steam");
                Console.WriteLine("[3] Launch Dev Build With Basic Commands");
                Console.WriteLine("[4] Launch Dev Build With Basic Commands - No Steam");
                Console.WriteLine("[5] Launch Dev Build in Performance Mode");
                Console.WriteLine("[6] Launch Dev Build in Performance Mode - No Steam");
                Console.WriteLine("[7] Launch Dev Build With Max Graphics");
                Console.WriteLine("[8] Launch Dev Build With Max Graphics - No Steam");
                Console.WriteLine("[9] Exit");

                Console.Write("Enter your choice: ");
                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        LaunchDevBuild(gamePath);
                        break;
                    case "2":
                        LaunchDevBuildNoSteam(gamePath);
                        break;
                    case "3":
                        LaunchDevBuildWithCommands(gamePath);
                        break;
                    case "4":
                        LaunchDevBuildWithCommandsNoSteam(gamePath);
                        break;
                    case "5":
                        LaunchDevBuildInPerformanceMode(gamePath);
                        break;
                    case "6":
                        LaunchDevBuildInPerformanceModeNoSteam(gamePath);
                        break;
                    case "7":
                        LaunchDevBuildMaxGraphics(gamePath);
                        break;
                    case "8":
                        LaunchDevBuildMaxGraphicsNoSteam(gamePath);
                        break;
                    case "9":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void LaunchDevBuild(string? gamePath)
        {
            Console.Clear();
            Console.WriteLine("Launching Dev Build!");
            Process.Start(new ProcessStartInfo()
            {
                FileName = gamePath + "DeadByDaylight.exe",
                UseShellExecute = true
            });
            PostLaunchMenu();
        }

        static void LaunchDevBuildNoSteam(string? gamePath)
        {
            Console.Clear();
            Console.WriteLine("Launching Dev Build - No Steam!");
            Process.Start(new ProcessStartInfo()
            {
                FileName = gamePath + "DeadByDaylight.exe",
                Arguments = "-nosteam",
                UseShellExecute = true
            });
            PostLaunchMenu();
        }

        static void LaunchDevBuildWithCommands(string? gamePath)
        {
            Console.Clear();
            Console.WriteLine("Launching Dev Build With Basic Commands!");
            Process.Start(new ProcessStartInfo()
            {
                FileName = gamePath + "DeadByDaylight.exe",
                Arguments = "-LoadPdbOnStartup=1 -ExecCmds=\"dbd.unlockAllCustomizationItems 1\",\"dbd.disableDlcChecks 1\",\"dbd.ignoregameendconditions 1\",\"dbd.ShowVersionNumber 0\",\"DisableAllScreenMessages\"",
                UseShellExecute = true
            });
            PostLaunchMenu();
        }

        static void LaunchDevBuildWithCommandsNoSteam(string? gamePath)
        {
            Console.Clear();
            Console.WriteLine("Launching Dev Build With Basic Commands - No Steam!");
            Process.Start(new ProcessStartInfo()
            {
                FileName = gamePath + "DeadByDaylight.exe",
                Arguments = "-LoadPdbOnStartup=1 -nosteam -ExecCmds=\"dbd.unlockAllCustomizationItems 1\",\"dbd.disableDlcChecks 1\",\"dbd.ignoregameendconditions 1\",\"dbd.ShowVersionNumber 0\",\"DisableAllScreenMessages\"",
                UseShellExecute = true
            });
            PostLaunchMenu();
        }

        static void LaunchDevBuildInPerformanceMode(string? gamePath)
        {
            Console.Clear();
            Console.WriteLine("Launching Dev Build in Performance Mode! (press f2 in-game for a bigger FPS boost)");
            Process.Start(new ProcessStartInfo()
            {
                FileName = gamePath + "DeadByDaylight.exe",
                Arguments = "-LoadPdbOnStartup=1 -ExecCmds=\"dbd.unlockAllCustomizationItems 1\",\"dbd.disableDlcChecks 1\",\"dbd.ignoregameendconditions 1\",\"dbd.ShowVersionNumber 0\",\"DisableAllScreenMessages\",\"IdealLightMapDensity 0.02\",\"MaxLightMapDensity 0.05\",\"gc.TimeBetweenPurgingPendingKillObjects 30\",\"foliage.LODDistanceScale 0.9\",\"foliage.DensityScale 0\",\"r.ViewDistanceScale 0.845\",\"r.AllowLandscapeShadows 0\",\"r.EmitterSpawnRateScale 0.825\",\"r.SeparateTranslucency 0\",\"r.DefaultFeature.Bloom 0\",\"r.DefaultFeature.AmbientOcclusion 0\",\"r.DefaultFeature.AmbientOcclusionStaticFraction 0\",\"r.DefaultFeature.MotionBlur 0\",\"r.DefaultFeature.LensFlare 0\",\"r.DefaultFeature.AntiAliasing 0\",\"r.DistanceFieldShadowing 0\",\"r.DistanceFieldAO 0\",\"r.MipMapLODBias 0\",\"r.LightShaftQuality 0\",\"r.SSS.Scale 0\",\"r.SSS.SampleSet 0\",\"r.SSS.Quality 0\",\"r.SkeletalMeshLODBias 2\",\"r.StaticMeshLODDistanceScale 2.35\",\"r.ShadowQuality 0\",\"r.Shadow.CSM.MaxCascades 0\",\"r.Shadow.MaxResolution 16\",\"Shadow.DistanceScale 0.85\",\"r.DepthOfFieldQuality 0\",\"r.RenderTargetPoolMin 512\",\"r.Upscale.Quality 0\",\"r.MaxAnisotropy 0\",\"r.TranslucencyLightingVolumeDim 1\",\"r.SceneColorFormat 3\",\"r.ParticleLightQuality 0\",\"r.LightFunctionQuality 0\",\"r.VSync 0\",\"t.MaxFPS 360\",\"sg.ShadowQuality 0\",\"sg.FoliageQuality 0\",\"sg.ViewDistanceQuality 0\",\"sg.PostProcessQuality 0\",\"sg.EffectsQuality 0\",\"sg.TextureQuality 0\",\"sg.ResolutionQuality 65\"",
                UseShellExecute = true
            });
            PostLaunchMenu();
        }

        static void LaunchDevBuildInPerformanceModeNoSteam(string? gamePath)
        {
            Console.Clear();
            Console.WriteLine("Launching Dev Build in Performance Mode - No Steam (press f2 in-game for a bigger FPS boost)");
            Process.Start(new ProcessStartInfo()
            {
                FileName = gamePath + "DeadByDaylight.exe",
                Arguments = "-LoadPdbOnStartup=1 -nosteam -ExecCmds=\"dbd.unlockAllCustomizationItems 1\",\"dbd.disableDlcChecks 1\",\"dbd.ignoregameendconditions 1\",\"dbd.ShowVersionNumber 0\",\"DisableAllScreenMessages\",\"IdealLightMapDensity 0.02\",\"MaxLightMapDensity 0.05\",\"gc.TimeBetweenPurgingPendingKillObjects 30\",\"foliage.LODDistanceScale 0.9\",\"foliage.DensityScale 0\",\"r.ViewDistanceScale 0.845\",\"r.AllowLandscapeShadows 0\",\"r.EmitterSpawnRateScale 0.825\",\"r.SeparateTranslucency 0\",\"r.DefaultFeature.Bloom 0\",\"r.DefaultFeature.AmbientOcclusion 0\",\"r.DefaultFeature.AmbientOcclusionStaticFraction 0\",\"r.DefaultFeature.MotionBlur 0\",\"r.DefaultFeature.LensFlare 0\",\"r.DefaultFeature.AntiAliasing 0\",\"r.DistanceFieldShadowing 0\",\"r.DistanceFieldAO 0\",\"r.MipMapLODBias 0\",\"r.LightShaftQuality 0\",\"r.SSS.Scale 0\",\"r.SSS.SampleSet 0\",\"r.SSS.Quality 0\",\"r.SkeletalMeshLODBias 2\",\"r.StaticMeshLODDistanceScale 2.35\",\"r.ShadowQuality 0\",\"r.Shadow.CSM.MaxCascades 0\",\"r.Shadow.MaxResolution 16\",\"Shadow.DistanceScale 0.85\",\"r.DepthOfFieldQuality 0\",\"r.RenderTargetPoolMin 512\",\"r.Upscale.Quality 0\",\"r.MaxAnisotropy 0\",\"r.TranslucencyLightingVolumeDim 1\",\"r.SceneColorFormat 3\",\"r.ParticleLightQuality 0\",\"r.LightFunctionQuality 0\",\"r.VSync 0\",\"t.MaxFPS 360\",\"sg.ShadowQuality 0\",\"sg.FoliageQuality 0\",\"sg.ViewDistanceQuality 0\",\"sg.PostProcessQuality 0\",\"sg.EffectsQuality 0\",\"sg.TextureQuality 0\"sg.ResolutionQuality 65\"",
                UseShellExecute = true
            });
            PostLaunchMenu();
        }

        static void LaunchDevBuildMaxGraphics(string? gamePath)
        {
            Console.Clear();
            Console.WriteLine("Launching Dev Build With Max Graphics");
            Process.Start(new ProcessStartInfo()
            {
                FileName = gamePath + "DeadByDaylight.exe",
                Arguments = "-LoadPdbOnStartup=1 -Exec" +
                "" +
                "" +
                "" +
                "" +
                "Cmds=\"dbd.unlockAllCustomizationItems 1\",\"dbd.disableDlcChecks 1\",\"dbd.ignoregameendconditions 1\",\"dbd.ShowVersionNumber 0\",\"DisableAllScreenMessages\",\"foliage.DitheredLOD 0\",\"foliage.DensityScale 1.125\",\"grass.DensityScale 1.125\",\"r.TranslucencyLightingVolumeDim 64\",\"r.RefractionQuality 3\",\"r.SSR.Quality 3\",\"r.SceneColorFormat 4\",\"r.LightingDetailMode 200\",\"r.TranslucencyVolumeBlur 1\",\"r.MaterialQualityLevel 1\",\"r.SSS.Scale 1\",\"r.SSS.SampleSet 2\",\"r.SSS.Quality 1\",\"r.EmitterSpawnRateScale 1.125\",\"r.LightPropagationVolume 0\",\"r.ParticleLightQuality 3\",\"r.Streaming.MipBias 0\",\"r.MaxAnisotropy 16\",\"r.SkeletalMeshLODBias 0\",\"r.StaticMeshLODDistanceScale 0\",\"r.Streaming.LimitPoolSizeToVRAM 0\",\"r.Streaming.PoolSize 4096\",\"r.MotionBlurQuality 4\",\"r.AmbientOcclusionMipLevelFactor 0.2\",\"r.AmbientOcclusionMaxQuality 100\",\"r.AmbientOcclusionLevels 2\",\"r.AmbientOcclusionRadiusScale 1.215\",\"r.DepthOfFieldQuality 2\",\"r.RenderTargetPoolMin 400\",\"r.LensFlareQuality 2\",\"r.SceneColorFringeQuality 1\",\"r.EyeAdaptationQuality 2\",\"r.BloomQuality 5\",\"r.Bloom.Intensity 1.0\",\"r.Bloom.Scale 1.0\",\"r.FastBlurThreshold 100\",\"r.Upscale.Quality 3\",\"r.Tonemapper.GrainQuantization 1\",\"r.LightShaftQuality 2\",\"r.Filter.SizeScale 1\",\"r.TonemapperQuality 2\",\"r.ViewDistanceScale 2.15\",\"r.LightFunctionQuality 1\",\"r.ShadowQuality 5\",\"r.Shadow.CSM.MaxCascades 8\",\"r.Shadow.MaxCSMResolution 4096\",\"r.Shadow.MaxResolution 4096\",\"r.VolumetricFog 1\",\"r.LightMaxDrawDistanceScale 1.95\",\"r.DefaultFeature.AntiAliasing 2\",\"r.VSync 0\",\"t.MaxFPS 360\",\"sg.ShadowQuality 5\",\"sg.FoliageQuality 5\",\"sg.ViewDistanceQuality 5\",\"sg.PostProcessQuality 5\",\"sg.EffectsQuality 5\",\"sg.TextureQuality 5\"",
                UseShellExecute = true
            });
            PostLaunchMenu();
        }

        static void LaunchDevBuildMaxGraphicsNoSteam(string? gamePath)
        {
            Console.Clear();
            Console.WriteLine("Launching Dev Build With Max Graphics - No Steam!");
            Process.Start(new ProcessStartInfo()
            {
                FileName = gamePath + "DeadByDaylight.exe",
                Arguments = "-LoadPdbOnStartup=1 -nosteam -ExecCmds=\"dbd.unlockAllCustomizationItems 1\",\"dbd.disableDlcChecks 1\",\"dbd.ignoregameendconditions 1\",\"dbd.ShowVersionNumber 0\",\"DisableAllScreenMessages\",\"foliage.DitheredLOD 0\",\"foliage.DensityScale 1.125\",\"grass.DensityScale 1.125\",\"r.TranslucencyLightingVolumeDim 64\",\"r.RefractionQuality 3\",\"r.SSR.Quality 3\",\"r.SceneColorFormat 4\",\"r.LightingDetailMode 200\",\"r.TranslucencyVolumeBlur 1\",\"r.MaterialQualityLevel 1\",\"r.SSS.Scale 1\",\"r.SSS.SampleSet 2\",\"r.SSS.Quality 1\",\"r.EmitterSpawnRateScale 1.125\",\"r.LightPropagationVolume 0\",\"r.ParticleLightQuality 3\",\"r.Streaming.MipBias 0\",\"r.MaxAnisotropy 16\",\"r.SkeletalMeshLODBias 0\",\"r.StaticMeshLODDistanceScale 0\",\"r.Streaming.LimitPoolSizeToVRAM 0\",\"r.Streaming.PoolSize 4096\",\"r.MotionBlurQuality 4\",\"r.AmbientOcclusionMipLevelFactor 0.2\",\"r.AmbientOcclusionMaxQuality 100\",\"r.AmbientOcclusionLevels 2\",\"r.AmbientOcclusionRadiusScale 1.215\",\"r.DepthOfFieldQuality 2\",\"r.RenderTargetPoolMin 400\",\"r.LensFlareQuality 2\",\"r.SceneColorFringeQuality 1\",\"r.EyeAdaptationQuality 2\",\"r.BloomQuality 5\",\"r.Bloom.Intensity 1.0\",\"r.Bloom.Scale 1.0\",\"r.FastBlurThreshold 100\",\"r.Upscale.Quality 3\",\"r.Tonemapper.GrainQuantization 1\",\"r.LightShaftQuality 2\",\"r.Filter.SizeScale 1\",\"r.TonemapperQuality 2\",\"r.ViewDistanceScale 2.15\",\"r.LightFunctionQuality 1\",\"r.ShadowQuality 5\",\"r.Shadow.CSM.MaxCascades 8\",\"r.Shadow.MaxCSMResolution 4096\",\"r.Shadow.MaxResolution 4096\",\"r.VolumetricFog 1\",\"r.LightMaxDrawDistanceScale 1.95\",\"r.DefaultFeature.AntiAliasing 2\",\"r.VSync 0\",\"t.MaxFPS 360\",\"sg.ShadowQuality 5\",\"sg.FoliageQuality 5\",\"sg.ViewDistanceQuality 5\",\"sg.PostProcessQuality 5\",\"sg.EffectsQuality 5\",\"sg.TextureQuality 5\"",
                UseShellExecute = true
            });
            PostLaunchMenu();
        }

        static void PostLaunchMenu()
        {
            PrintLogo();
            Console.WriteLine("Dev Build Launched Successfully!");
            Console.WriteLine("[1] Return");
            Console.WriteLine("[2] Exit");

            Console.Write("Enter your choice: ");
            string? option = Console.ReadLine();

            if (option == "1")
            {
                MainMenu(System.IO.File.ReadAllText("gamepath.txt").Trim());
            }
            else if (option == "2")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid option. Returning to the main menu.");
                MainMenu(System.IO.File.ReadAllText("gamepath.txt").Trim());
            }
        }
    }
}
