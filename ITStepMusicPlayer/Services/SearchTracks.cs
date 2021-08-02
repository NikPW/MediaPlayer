// Kramer "AlexI-ST" Alexey

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions; 

namespace ITStepMusicPlayer.Services
{
    public static class SearchTracks
    {
        public static List<string> Search()
        {
            List<string> totalFile = new List<string>{};

            // string dirName = @"C:\Users\" + Environment.UserName;
            string dirName = @"D:\Music";
            
            if (Directory.Exists(dirName))
            {
                string[] dirs = Directory.GetDirectories(dirName);
                foreach (string s in dirs)
                {
                    if (s.Contains('.')) continue;
                        try
                        {
                            string[] directories = Directory.GetDirectories(s, ".", SearchOption.AllDirectories);
                            foreach (string d in directories)
                            {
                                if (d.Contains('.')) continue;

                                string[] files;
                                try {
                                    files = Directory.GetFiles(d, ".");
                                }
                                catch { continue; }
                                foreach (string file in files)
                                {
                                    
                                    if (file.Contains(Types.mp3.ToString()) || file.Contains(Types.flac.ToString()) || file.Contains(Types.aac.ToString()) || file.Contains(Types.wav.ToString()) || file.Contains(Types.alac.ToString()))
                                    {
                                        totalFile.Add(file);
                                    }
                                }
                            }
                            
                            
                        }
                        catch 
                        {
                            continue;
                        }

                }
            }
/*
            dirName = @"D:\";
            if (Directory.Exists(dirName))
            {
                string[] dirs = Directory.GetDirectories(dirName);
                foreach (string s in dirs)
                {
                    if (s.Contains('.')) continue;
                    try
                    {
                        string[] directories = Directory.GetDirectories(s, ".", SearchOption.AllDirectories);
                        foreach (string d in directories)
                        {
                            if (d.Contains('.')) continue;
                            string[] files;
                            try {
                                files = Directory.GetFiles(d, ".");
                            }
                            catch { continue; }

                            foreach (string file in files)
                            {
                                    
                                if (file.Contains(Types.mp3.ToString()) || file.Contains(Types.flac.ToString()) || file.Contains(Types.aac.ToString()) || file.Contains(Types.wav.ToString()) || file.Contains(Types.alac.ToString()))
                                {
                                    totalFile.Add(file);
                                }
                            }
                        }
                            
                            
                    }
                    catch 
                    {
                        continue;
                    }

                }
            }
            */
            return totalFile;
        }

        public enum Types
        {
            mp3, flac, wav, alac, aac
        }
    }
}