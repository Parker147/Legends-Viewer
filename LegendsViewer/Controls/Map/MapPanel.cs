using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LegendsViewer.Legends;
using LegendsViewer.Legends.EventCollections;
using LegendsViewer.Legends.Interfaces;
using SevenZip;

namespace LegendsViewer.Controls.Map
{
    public class MapPanel : Panel
    {
        public DwarfTabControl TabControl;
        Bitmap Map, Minimap;
        public Bitmap AlternateMap, Overlay;
        //Size MinimapSize;
        public object FocusObject;
        List<object> FocusObjects;
        World World;
        List<object> DisplayObjects;
        public Point MousePanStart, Center, MouseClickLocation, MouseLocation;
        public Rectangle Source;
        Rectangle ZoomBounds;
        public double ZoomCurrent = 1;
        public double PixelWidth, PixelHeight, ZoomChange = 0.15, ZoomMax = 10.0, ZoomMin = 0.2;
        MapMenu HoverMenu, ControlMenu, YearMenu;
        MapMenuHorizontal OptionsMenu;
        public bool ZoomToBoundsOnFirstPaint, CivsToggled, SitesToggled, WarsToggled, BattlesToggled, OverlayToggled, AlternateMapToggled;
        public int TileSize = 16, MinYear, MaxYear, CurrentYear, MiniMapAreaSideLength = 200;
        List<CivPaths> AllCivPaths = new List<CivPaths>();
        List<Entity> WarEntities = new List<Entity>();
        List<Battle> Battles = new List<Battle>();
        List<Location> BattleLocations = new List<Location>();
        TrackBar AltMapTransparency = new TrackBar();
        float AltMapAlpha = 1.0f;

        public MapPanel(Bitmap map, World world, DwarfTabControl dwarfTabControl, object focusObject)
        {
            TabControl = dwarfTabControl;
            Map = map;
            World = world;
            FocusObject = focusObject;
            if (FocusObject != null && FocusObject.GetType() == typeof(World))
                FocusObject = null;
            DisplayObjects = new List<object>();
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            //Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Source = new Rectangle(new Point(Center.X - (Width / 2), Center.Y - (Height / 2)), new Size(Width, Height));

            HoverMenu = new MapMenu(this);
            ControlMenu = new MapMenu(this);
            ControlMenu.AddOptions(new List<object> { "Zoom In", "Zoom Out", "Toggle Civs", "Toggle Sites", "Toggle Wars", "Toggle Battles", "Toggle Overlay", "Toggle Alt Map" });
            ControlMenu.Open = true;
            YearMenu = new MapMenu(this);
            YearMenu.AddOptions(new List<object> { "+1000", "+100", "+10", "+1", "-1", "-10", "-100", "-1000" });
            YearMenu.Open = true;
            OptionsMenu = new MapMenuHorizontal(this);
            OptionsMenu.Open = true;
            OptionsMenu.AddOptions(new List<object> { "Load Alternate Map...", "Overlays" });
            MapMenu overlayOptions = new MapMenu(this);
            overlayOptions.AddOptions(new List<object> { "Battles", "Battles (Notable)", "Battle Deaths", "Beast Attacks", "Site Population...", "Site Events", "Site Events (Filtered)" });
            overlayOptions.Options.ForEach(option => option.OptionObject = "Overlay");
            OptionsMenu.Options.Last().SubMenu = overlayOptions;

            AltMapTransparency.Minimum = 0;
            AltMapTransparency.Maximum = 100;
            AltMapTransparency.AutoSize = false;
            AltMapTransparency.Size = new Size(150, 25);
            AltMapTransparency.TickFrequency = 1;
            AltMapTransparency.TickStyle = TickStyle.None;
            AltMapTransparency.BackColor = YearMenu.MenuColor;
            AltMapTransparency.Visible = false;
            AltMapTransparency.Scroll += ChangeAltMapTransparency;
            AltMapTransparency.Value = 100;
            Controls.Add(AltMapTransparency);
            //AltMapTransparency.Anchor = AnchorStyles.Bottom;
            AltMapTransparency.Location = new Point(MiniMapAreaSideLength + YearMenu.MenuBox.Width, Height - AltMapTransparency.Height);


            MinYear = World.Eras.First().StartYear;
            if (MinYear == -1) MinYear = 0;
            MaxYear = CurrentYear = World.Eras.Last().EndYear;
            //Set Map Year if Entity has no active sites so they show on map
            if (FocusObject != null && FocusObject.GetType() == typeof(Entity) && ((Entity)FocusObject).SiteHistory.Count(sitePeriod => sitePeriod.EndYear == -1) == 0)
                CurrentYear = (FocusObject as Entity).SiteHistory.Max(sitePeriod => sitePeriod.EndYear) - 1;
            else if (FocusObject != null && FocusObject.GetType() == typeof(Battle))
                CurrentYear = MinYear = MaxYear = ((Battle)focusObject).StartYear;
            else if (FocusObject != null && FocusObject.GetType() == typeof(SiteConquered))
                CurrentYear = MinYear = MaxYear = ((SiteConquered)FocusObject).StartYear;

            if (focusObject != null && focusObject.GetType() == typeof(List<object>))
            {
                FocusObjects = ((List<object>)focusObject).GroupBy(item => item).Select(item => item.Key).ToList();
                if (FocusObjects.First().GetType() == typeof(Battle))
                    Battles.AddRange(FocusObjects.Cast<Battle>());
                else
                    DisplayObjects.AddRange(FocusObjects);
            }
            else
                FocusObjects = new List<object>();
            if (focusObject != null && focusObject.GetType() != typeof(Battle)) DisplayObjects.Add(focusObject);
            if (focusObject != null && focusObject.GetType() == typeof(Battle)) Battles.Add(focusObject as Battle);
            if (FocusObject != null && FocusObject.GetType() == typeof(War))
            {
                War war = FocusObject as War;
                if (war != null)
                {
                    MinYear = CurrentYear = war.StartYear;
                    if (war.EndYear != -1) MaxYear = war.EndYear;
                    UpdateWarDisplay();
                    foreach (Battle battle in war.Collections.OfType<Battle>())
                        Battles.Add(battle);
                }
            }

            //Center and zoom map on focusObject of the map
            if (FocusObject == null || FocusObjects.Count > 0) Center = new Point(Map.Width / 2, Map.Height / 2);
            else if (FocusObject.GetType() == typeof(Site))
            {
                Site site = (focusObject as Site);
                Center = new Point(site.Coordinates.X * TileSize + TileSize / 2, site.Coordinates.Y * TileSize + TileSize / 2);
                ZoomCurrent = 0.85;
            }
            else if (FocusObject.GetType() == typeof(Entity) || FocusObject.GetType() == typeof(War)
                     || FocusObject.GetType() == typeof(Battle) || FocusObject.GetType() == typeof(SiteConquered)
                     || FocusObject.GetType() == typeof(WorldRegion) || FocusObject.GetType() == typeof(UndergroundRegion) 
                     || FocusObject.GetType() == typeof(WorldConstruction))
            {
                List<Entity> entities = new List<Entity>();
                if (FocusObject.GetType() == typeof(Entity))
                    entities.Add(FocusObject as Entity);
                else if (FocusObject.GetType() == typeof(War))
                {
                    entities.Add((FocusObject as War).Attacker);
                    entities.Add((FocusObject as War).Defender);
                }
                ZoomBounds = new Rectangle(-1, -1, -1, -1);

                foreach (Entity displayEntity in entities)
                    foreach (OwnerPeriod sitePeriod in displayEntity.SiteHistory.Where(site => ((site.StartYear == CurrentYear && site.StartCause != "took over") || site.StartYear < CurrentYear)
                                                                                               && (((site.EndYear >= CurrentYear) || site.EndYear == -1))))
                    {
                        if (ZoomBounds.Top == -1)
                        {
                            ZoomBounds.Y = ZoomBounds.Height = sitePeriod.Site.Coordinates.Y;
                            ZoomBounds.X = ZoomBounds.Width = sitePeriod.Site.Coordinates.X;
                        }
                        if (sitePeriod.Site.Coordinates.Y < ZoomBounds.Y) ZoomBounds.Y = sitePeriod.Site.Coordinates.Y;
                        if (sitePeriod.Site.Coordinates.X < ZoomBounds.X) ZoomBounds.X = sitePeriod.Site.Coordinates.X;
                        if (sitePeriod.Site.Coordinates.Y > ZoomBounds.Height) ZoomBounds.Height = sitePeriod.Site.Coordinates.Y;
                        if (sitePeriod.Site.Coordinates.X > ZoomBounds.Width) ZoomBounds.Width = sitePeriod.Site.Coordinates.X;
                    }

                if (FocusObject.GetType() == typeof(War))
                {
                    War war = FocusObject as War;

                    foreach (Battle battle in war.Collections.OfType<Battle>())
                    {
                        if (ZoomBounds.Top == -1)
                        {
                            ZoomBounds.Y = ZoomBounds.Height = battle.Coordinates.Y;
                            ZoomBounds.X = ZoomBounds.Width = battle.Coordinates.X;
                        }
                        if (battle.Coordinates.Y < ZoomBounds.Y) ZoomBounds.Y = battle.Coordinates.Y;
                        if (battle.Coordinates.X < ZoomBounds.X) ZoomBounds.X = battle.Coordinates.X;
                        if (battle.Coordinates.Y > ZoomBounds.Height) ZoomBounds.Height = battle.Coordinates.Y;
                        if (battle.Coordinates.X > ZoomBounds.Width) ZoomBounds.Width = battle.Coordinates.X;
                    }
                }

                if (FocusObject.GetType() == typeof(Battle) || FocusObject.GetType() == typeof(SiteConquered))
                {
                    Battle battle;
                    if (FocusObject.GetType() == typeof(Battle)) battle = FocusObject as Battle;
                    else battle = (FocusObject as SiteConquered).Battle;
                    Center = new Point(battle.Coordinates.X * TileSize + TileSize / 2, battle.Coordinates.Y * TileSize + TileSize / 2);
                    ZoomBounds.Y = ZoomBounds.Height = battle.Coordinates.Y;
                    ZoomBounds.X = ZoomBounds.Width = battle.Coordinates.X;
                    Site attackerSite = GetClosestSite(battle.Attacker, battle.Coordinates);
                    if (attackerSite.Coordinates.Y < ZoomBounds.Y) ZoomBounds.Y = attackerSite.Coordinates.Y;
                    if (attackerSite.Coordinates.X < ZoomBounds.X) ZoomBounds.X = attackerSite.Coordinates.X;
                    if (attackerSite.Coordinates.Y > ZoomBounds.Height) ZoomBounds.Height = attackerSite.Coordinates.Y;
                    if (attackerSite.Coordinates.X > ZoomBounds.Width) ZoomBounds.Width = attackerSite.Coordinates.X;

                    Site defenderSite = GetClosestSite(battle.Defender, battle.Coordinates);
                    if (defenderSite.Coordinates.Y < ZoomBounds.Y) ZoomBounds.Y = defenderSite.Coordinates.Y;
                    if (defenderSite.Coordinates.X < ZoomBounds.X) ZoomBounds.X = defenderSite.Coordinates.X;
                    if (defenderSite.Coordinates.Y > ZoomBounds.Height) ZoomBounds.Height = defenderSite.Coordinates.Y;
                    if (defenderSite.Coordinates.X > ZoomBounds.Width) ZoomBounds.Width = defenderSite.Coordinates.X;
                }

                if (FocusObject is IHasCoordinates)
                {
                    ZoomBounds.X = (FocusObject as IHasCoordinates).Coordinates.Min(coord => coord.X);
                    ZoomBounds.Y = (FocusObject as IHasCoordinates).Coordinates.Min(coord => coord.Y);
                    ZoomBounds.Width = (FocusObject as IHasCoordinates).Coordinates.Max(coord => coord.X);
                    ZoomBounds.Height = (FocusObject as IHasCoordinates).Coordinates.Max(coord => coord.Y);
                }

                ZoomBounds.X = ZoomBounds.X * TileSize - TileSize / 2 - TileSize;
                ZoomBounds.Width = ZoomBounds.Width * TileSize + TileSize / 2 + TileSize;
                ZoomBounds.Y = ZoomBounds.Y * TileSize - TileSize / 2 - TileSize;
                ZoomBounds.Height = ZoomBounds.Height * TileSize + TileSize / 2 + TileSize;
                Center.X = (ZoomBounds.Left + ZoomBounds.Width) / 2;
                Center.Y = (ZoomBounds.Top + ZoomBounds.Height) / 2;
                ZoomToBoundsOnFirstPaint = true;
            }
            else
                Center = new Point(0, 0);

            BattleLocations = Battles.GroupBy(battle => battle.Coordinates).Select(battle => battle.Key).ToList();

            GenerateCivPaths();
            Minimap = world.MiniMap;
            UpdateWarDisplay();
            Invalidate();

            if (MapUtil.AlternateMap != null)
            {
                AlternateMap = MapUtil.AlternateMap;
                AltMapAlpha = MapUtil.AltMapAlpha;
                AltMapTransparency.Value = (int)(AltMapAlpha * 100);
                AlternateMapToggled = false;
                ToggleAlternateMap();
            }
        }

        private class CivPaths
        {
            public Entity Civ;
            public List<List<Site>> SitePaths;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (ZoomToBoundsOnFirstPaint) ZoomToBounds();
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            DrawMap(e.Graphics);
            DrawDisplayObjects(e.Graphics);
            DrawOverlay(e.Graphics);
            ControlMenu.Draw(e.Graphics);
            YearMenu.Draw(e.Graphics);
            OptionsMenu.Draw(e.Graphics);
            DrawInfo(e.Graphics);
            DrawMiniMap(e.Graphics);
            if (HoverMenu.Options.Count > 0)
                HoverMenu.Draw(e.Graphics);
        }

        private void DrawMap(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 50)), new Rectangle(Location, Size));

            if (AlternateMapToggled && AltMapAlpha > 0)
            {
                if (AltMapAlpha < 1)
                {
                    g.DrawImage(Map, ClientRectangle, Source, GraphicsUnit.Pixel);
                    using (ImageAttributes adjustAlpha = new ImageAttributes())
                    {
                        ColorMatrix adjustAlphaMatrix = new ColorMatrix();
                        adjustAlphaMatrix.Matrix00 = adjustAlphaMatrix.Matrix11 = adjustAlphaMatrix.Matrix22 = adjustAlphaMatrix.Matrix44 = 1.0f;
                        adjustAlphaMatrix.Matrix33 = AltMapAlpha;
                        adjustAlpha.SetColorMatrix(adjustAlphaMatrix);
                        g.DrawImage(AlternateMap, ClientRectangle, Source.X, Source.Y, Source.Width, Source.Height, GraphicsUnit.Pixel, adjustAlpha);
                    }

                }
                else
                    g.DrawImage(AlternateMap, ClientRectangle, Source, GraphicsUnit.Pixel);
            }
            else g.DrawImage(Map, ClientRectangle, Source, GraphicsUnit.Pixel);

        }

        private void DrawMiniMap(Graphics g)
        {
            double minimapRatio;
            if (Map.Width > Map.Height) minimapRatio = Convert.ToDouble(Minimap.Width) / Convert.ToDouble(Map.Width);
            else minimapRatio = Convert.ToDouble(Minimap.Height) / Convert.ToDouble(Map.Height);
            Rectangle minimapArea = new Rectangle(0, Height - MiniMapAreaSideLength, MiniMapAreaSideLength, MiniMapAreaSideLength);
            Point miniMapDrawLocation = new Point(minimapArea.X + MiniMapAreaSideLength / 2 - Minimap.Width / 2, minimapArea.Y + MiniMapAreaSideLength / 2 - Minimap.Height / 2);
            g.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 50)), minimapArea);
            g.DrawImage(Minimap, new Rectangle(miniMapDrawLocation.X, miniMapDrawLocation.Y, Minimap.Width, Minimap.Height), new Rectangle(0, 0, Minimap.Width, Minimap.Height), GraphicsUnit.Pixel);

            //Point miniSourceLocation = new Point(minimapArea.X + Convert.ToInt32(Source.X * minimapRatio), minimapArea.Y + Convert.ToInt32(Source.Y * minimapRatio));
            Point miniSourceLocation = new Point(miniMapDrawLocation.X + Convert.ToInt32(Source.X * minimapRatio), miniMapDrawLocation.Y + Convert.ToInt32(Source.Y * minimapRatio));
            Size miniSourceSize = new Size(Convert.ToInt32(Source.Width * minimapRatio), Convert.ToInt32(Source.Height * minimapRatio));
            Rectangle miniSource = new Rectangle(miniSourceLocation, miniSourceSize);

            if (miniSource.Left < minimapArea.Left) { miniSource.Width -= minimapArea.Left - miniSource.Left; miniSource.X = minimapArea.Left; }
            if (miniSource.Top < minimapArea.Top) { miniSource.Height -= minimapArea.Top - miniSource.Top; miniSource.Y = minimapArea.Top; }

            if (miniSource.Right > minimapArea.Right) miniSource.Width = minimapArea.X + Minimap.Width - miniSource.X;
            if (miniSource.Bottom > minimapArea.Bottom) miniSource.Height = minimapArea.Y + Minimap.Height - miniSource.Y;

            g.SmoothingMode = SmoothingMode.None;
            g.PixelOffsetMode = PixelOffsetMode.Default;
            using (Pen minimapPen = new Pen(Color.White))
                g.DrawRectangle(minimapPen, miniSource);
        }

        public void DrawDisplayObjects(Graphics g)
        {
            SizeF scaleTileSize = new SizeF((float)(PixelWidth * TileSize), (float)(PixelHeight * TileSize));
            Pen sitePen = new Pen(Color.White);
            g.PixelOffsetMode = PixelOffsetMode.Half;
            foreach (Site site in DisplayObjects.OfType<Site>())
            {
                PointF siteLocation = new PointF();
                siteLocation.X = (float)((site.Coordinates.X * TileSize - Source.X) * PixelWidth + 2 * PixelWidth);
                siteLocation.Y = (float)((site.Coordinates.Y * TileSize - Source.Y) * PixelHeight + 2 * PixelHeight);
                g.DrawRectangle(sitePen, siteLocation.X, siteLocation.Y, scaleTileSize.Width - (float)(4 * PixelWidth), scaleTileSize.Height - (float)(4 * PixelHeight));
            }
            sitePen.Dispose();



            DrawEntities(g, DisplayObjects.OfType<Entity>().ToList());
            DrawCivPaths(g);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.PixelOffsetMode = PixelOffsetMode.Half;
            /*var battleLocations = from battle in DisplayObjects.OfType<Battle>()
                                  group battle by battle.Coordinates into battleLocation
                                  select new { Coordinates = battleLocation.Key };*/

            foreach (var battle in BattleLocations)
            {
                PointF battleLocation = new PointF
                {
                    X = (float) ((battle.X*TileSize - Source.X)*PixelWidth),
                    Y = (float) ((battle.Y*TileSize - Source.Y)*PixelHeight)
                };
                using (Pen battlePen = new Pen(Color.FromArgb(175, Color.White), 2))
                    g.DrawEllipse(battlePen, battleLocation.X + scaleTileSize.Width / 4, battleLocation.Y + scaleTileSize.Height / 4, scaleTileSize.Width / 2, scaleTileSize.Height / 2);
            }

            if (FocusObject != null && FocusObject is IHasCoordinates)
            {
                foreach (Location coord in ((IHasCoordinates) FocusObject).Coordinates)
                {
                    PointF loc = new PointF
                    {
                        X = (float) ((coord.X*TileSize - Source.X)*PixelWidth),
                        Y = (float) ((coord.Y*TileSize - Source.Y)*PixelHeight)
                    };

                    using (Pen pen = new Pen(Color.FromArgb(255, Color.White), 2))
                    {
                        if (FocusObject.GetType() == typeof(WorldRegion))
                        {
                            g.FillRectangle(new SolidBrush(Color.LightGreen), loc.X, loc.Y,
                                scaleTileSize.Width - (float)(4 * PixelWidth), scaleTileSize.Height - (float)(4 * PixelHeight));
                        }
                        else if (FocusObject.GetType() == typeof(UndergroundRegion))
                        {
                            g.FillRectangle(new SolidBrush(Color.SandyBrown), loc.X, loc.Y,
                                scaleTileSize.Width - (float)(4 * PixelWidth), scaleTileSize.Height - (float)(4 * PixelHeight));
                        }
                        else if (FocusObject.GetType() == typeof(WorldConstruction))
                        {
                            g.FillRectangle(new SolidBrush(Color.Gold), loc.X, loc.Y,
                                scaleTileSize.Width - (float)(4 * PixelWidth), scaleTileSize.Height - (float)(4 * PixelHeight));
                        }
                        g.DrawRectangle(pen, loc.X, loc.Y,
                            scaleTileSize.Width - (float)(4 * PixelWidth), scaleTileSize.Height - (float)(4 * PixelHeight));
                    }
                }
            }

            if (FocusObject != null && FocusObject.GetType() == typeof(Battle)) DrawBattlePaths(g, FocusObject as Battle);
            if (FocusObject != null && FocusObject.GetType() == typeof(SiteConquered)) DrawBattlePaths(g, ((SiteConquered)FocusObject).Battle);

            foreach (War war in DisplayObjects.OfType<War>().Where(war => war.StartYear <= CurrentYear && (war.EndYear >= CurrentYear || war.EndYear == -1)))
                foreach (Battle battle in war.Collections.OfType<Battle>().Where(battle => battle.StartYear == CurrentYear))
                    DrawBattlePaths(g, battle);
        }

        private void DrawOverlay(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            if (OverlayToggled)
                g.DrawImage(Overlay, ClientRectangle, Source, GraphicsUnit.Pixel);
        }

        private void DrawEntities(Graphics g, List<Entity> entities)
        {
            SizeF scaleTileSize = new SizeF((float)PixelWidth * TileSize, (float)PixelHeight * TileSize);
            g.InterpolationMode = InterpolationMode.Default;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            foreach (Entity civ in entities)
            {
                foreach (OwnerPeriod site in civ.SiteHistory.Where(site => ((site.StartYear == CurrentYear && site.StartCause != "took over") || site.StartYear < CurrentYear)
                                                                           && (((site.EndYear >= CurrentYear) || site.EndYear == -1))))
                {
                    PointF siteLocation = new PointF();
                    siteLocation.X = (float)((site.Site.Coordinates.X * TileSize - Source.X) * PixelWidth - 0);
                    siteLocation.Y = (float)((site.Site.Coordinates.Y * TileSize - Source.Y) * PixelHeight - 0);
                    if (site.EndYear == CurrentYear)
                    {
                        ColorMatrix cm = new ColorMatrix();
                        cm.Matrix00 = cm.Matrix11 = cm.Matrix22 = cm.Matrix44 = 0.66f;
                        cm.Matrix33 = 1f;
                        ImageAttributes ia = new ImageAttributes();
                        ia.SetColorMatrix(cm);
                        using (Bitmap lostSiteIdenticon = new Bitmap(civ.Identicon.Width, civ.Identicon.Height))
                        {
                            using (Graphics drawLostSite = Graphics.FromImage(lostSiteIdenticon))
                            {
                                drawLostSite.DrawImage(civ.Identicon, new Rectangle(0, 0, civ.Identicon.Width, civ.Identicon.Height), 0, 0, civ.Identicon.Width, civ.Identicon.Height, GraphicsUnit.Pixel, ia);
                            }
                            g.DrawImage(lostSiteIdenticon, new RectangleF(siteLocation.X, siteLocation.Y, scaleTileSize.Width + 1, scaleTileSize.Height + 1));
                        }
                    }
                    else
                        g.DrawImage(civ.Identicon, new RectangleF(siteLocation.X, siteLocation.Y, scaleTileSize.Width + 1, scaleTileSize.Height + 1));
                }
            }
        }

        private void DrawBattlePaths(Graphics g, Battle battle)
        {
            using (Pen attackerLine = new Pen(Color.Black, 3))
            using (Pen defenderLine = new Pen(Color.Black, 3))
            using (AdjustableArrowCap victorCap = new AdjustableArrowCap(4, 6))
            using (AdjustableArrowCap loserCap = new AdjustableArrowCap(5, 6))
            {
                attackerLine.DashStyle = DashStyle.Dot;
                defenderLine.DashStyle = DashStyle.Dot;

                loserCap.Filled = false;

                Site attackerSite = GetClosestSite(battle.Attacker, battle.Coordinates);
                Site defenderSite = GetClosestSite(battle.Defender, battle.Coordinates);

                attackerLine.Color = battle.Attacker.LineColor;
                defenderLine.Color = battle.Defender.LineColor;
                if (battle.Victor == battle.Attacker)
                {
                    attackerLine.CustomEndCap = victorCap;
                    defenderLine.CustomEndCap = loserCap;
                }
                else if (battle.Victor == battle.Defender)
                {
                    attackerLine.CustomEndCap = loserCap;
                    defenderLine.CustomEndCap = victorCap;
                }

                g.DrawLine(attackerLine, SiteToScreen(attackerSite.Coordinates), SiteToScreen(battle.Coordinates));
                if (defenderSite != null && defenderSite.Coordinates != battle.Coordinates)
                    g.DrawLine(defenderLine, SiteToScreen(defenderSite.Coordinates), SiteToScreen(battle.Coordinates));
            }
        }

        private void DrawCivPaths(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (CivPaths civPaths in AllCivPaths)
            {
                foreach (List<Site> path in civPaths.SitePaths)
                {
                    Site site1 = path.First();
                    Site site2 = path.Last();
                    Size tileDistance = new Size(site1.Coordinates.X - site2.Coordinates.X, site1.Coordinates.Y - site2.Coordinates.Y);
                    if (tileDistance.Width < 0) tileDistance.Width *= -1;
                    if (tileDistance.Height < 0) tileDistance.Height *= -1;

                    if ((tileDistance.Width > 1 || tileDistance.Height > 1))
                        using (Pen pathPen = new Pen(civPaths.Civ.LineColor, 2))
                            g.DrawLine(pathPen, SiteToScreen(site1.Coordinates), SiteToScreen(site2.Coordinates));
                }
            }
        }

        private void DrawInfo(Graphics g)
        {
            Point tileLocation = WindowToTilePoint(MouseLocation);
            Font font = new Font("Arial", 10);
            Brush fontBrush = new SolidBrush(Color.Gray);
            Brush boxBrush = new SolidBrush(Color.FromArgb(200, Color.Black));

            SizeF tileInfoSize = g.MeasureString(tileLocation.X + ", " + tileLocation.Y, font);
            Rectangle tileInfoBox = new Rectangle(ControlMenu.MenuBox.Width, Height - Minimap.Height - Convert.ToInt32(tileInfoSize.Height), 70, Convert.ToInt32(tileInfoSize.Height));
            g.FillRectangle(boxBrush, tileInfoBox);
            g.DrawString(tileLocation.X + ", " + tileLocation.Y, font, fontBrush, new Point(tileInfoBox.X + 3, tileInfoBox.Y));

            SizeF yearBoxSize = g.MeasureString(CurrentYear.ToString(), font);
            Rectangle yearBox = new Rectangle(Minimap.Width, Height - YearMenu.MenuBox.Height - Convert.ToInt32(yearBoxSize.Height), YearMenu.MenuBox.Width, Convert.ToInt32(yearBoxSize.Height));
            g.FillRectangle(boxBrush, yearBox);
            g.DrawString(CurrentYear.ToString(), font, fontBrush, new Point(yearBox.X + 3, yearBox.Y));

            font.Dispose();
            fontBrush.Dispose();
            boxBrush.Dispose();

        }

        public void ToggleCivs()
        {
            bool reToggleWars = false;
            if (WarsToggled)
            {
                ToggleWars();
                reToggleWars = true;
            }

            if (!CivsToggled)
            {
                DisplayObjects.AddRange(World.Entities.Where(entity => entity.IsCiv && entity != FocusObject && !DisplayObjects.Contains(entity)));
                CivsToggled = true;
            }
            else
            {
                DisplayObjects.RemoveAll(dwarfObject => dwarfObject.GetType() == typeof(Entity) && dwarfObject != FocusObject && !WarEntities.Contains(dwarfObject));
                CivsToggled = false;
            }

            ControlMenu.Options.Single(option => option.Text == "Toggle Civs").Toggled = CivsToggled;
            GenerateCivPaths();
            if (reToggleWars) ToggleWars();
            Invalidate();

        }

        public void ToggleSites()
        {
            if (!SitesToggled)
            {
                DisplayObjects.AddRange(World.Sites.Where(site => site != FocusObject));
                SitesToggled = true;
            }
            else
            {
                DisplayObjects.RemoveAll(dwarfObject => dwarfObject.GetType() == typeof(Site) && dwarfObject != FocusObject && !FocusObjects.Contains(dwarfObject));
                SitesToggled = false;
            }

            ControlMenu.Options.Single(option => option.Text == "Toggle Sites").Toggled = SitesToggled;
            Invalidate();
        }

        public void ToggleWars()
        {
            if (!WarsToggled)
            {
                List<War> displayWars = new List<War>();
                foreach (Entity entity in DisplayObjects.OfType<Entity>())
                    foreach (War war in entity.Wars)
                        displayWars.Add(war);
                foreach (War war in displayWars)
                    if (!DisplayObjects.Contains(war) && war != FocusObject) DisplayObjects.Add(war);
                WarsToggled = true;
            }
            else
            {
                DisplayObjects.RemoveAll(displayObject => displayObject.GetType() == typeof(War) && displayObject != FocusObject);
                WarsToggled = false;
            }
            UpdateWarDisplay();
            ControlMenu.Options.Single(option => option.Text == "Toggle Wars").Toggled = WarsToggled;
            Invalidate();
        }

        public void ToggleBattles()
        {
            if (!BattlesToggled)
            {
                //DisplayObjects.AddRange(World.EventCollections.OfType<Battle>().Where(battle => !DisplayObjects.Contains(battle) && battle != FocusObject));
                Battles.AddRange(World.EventCollections.OfType<Battle>().Where(battle => !DisplayObjects.Contains(battle) && battle != FocusObject && !FocusObjects.Contains(battle)));
                BattleLocations = Battles.GroupBy(battle => battle.Coordinates).Select(battle => battle.Key).ToList();
                BattlesToggled = true;
            }
            else
            {
                //DisplayObjects.RemoveAll(displayObject => displayObject.GetType() == typeof(Battle) && displayObject != FocusObject);
                Battles.Clear();
                if (FocusObjects.Count > 0 && FocusObjects.First().GetType() == typeof(Battle)) Battles.AddRange(FocusObjects.Cast<Battle>());
                if (FocusObject != null && FocusObject.GetType() == typeof(Battle)) Battles.Add(FocusObject as Battle);
                if (FocusObject != null && FocusObject.GetType() == typeof(War))
                {
                    //DisplayObjects.AddRange((FocusObject as War).Collections.OfType<Battle>());
                    Battles.AddRange(((War)FocusObject).Collections.OfType<Battle>());

                }
                //BattleLocations = DisplayObjects.OfType<Battle>().GroupBy(battle => battle.Coordinates).Select(battle => battle.Key).ToList();
                BattleLocations = Battles.GroupBy(battle => battle.Coordinates).Select(battle => battle.Key).ToList();
                BattlesToggled = false;
            }
            ControlMenu.Options.Single(option => option.Text == "Toggle Battles").Toggled = BattlesToggled;
            Invalidate();
        }

        public void ToggleOverlay()
        {
            if (Overlay != null)
            {
                OverlayToggled = !OverlayToggled;
                Invalidate();
            }
            ControlMenu.Options.Single(option => option.Text == "Toggle Overlay").Toggled = OverlayToggled;
        }

        public void ToggleAlternateMap()
        {
            if (AlternateMap != null)
            {
                AlternateMapToggled = !AlternateMapToggled;
                Invalidate();
            }
            ControlMenu.Options.Single(option => option.Text == "Toggle Alt Map").Toggled = AlternateMapToggled;
            AltMapTransparency.Visible = AlternateMapToggled;
        }

        public void GenerateCivPaths()
        {
            AllCivPaths = new List<CivPaths>();
            foreach (Entity civ in DisplayObjects.OfType<Entity>())
            {
                CivPaths civPaths = new CivPaths();
                civPaths.Civ = civ;
                civPaths.SitePaths = PathMaker.Create(civ, CurrentYear);
                AllCivPaths.Add(civPaths);
            }
        }

        public void ChangeYear(int amount)
        {
            CurrentYear += amount;
            if (CurrentYear > MaxYear) CurrentYear = MaxYear;
            if (CurrentYear < MinYear) CurrentYear = MinYear;

            UpdateWarDisplay();
            GenerateCivPaths();
            Invalidate();
        }

        public void UpdateWarDisplay()
        {
            DisplayObjects.RemoveAll(displayObject => WarEntities.Contains(displayObject));
            WarEntities.Clear();
            foreach (War war in DisplayObjects.OfType<War>().Where(war => war.StartYear <= CurrentYear && (war.EndYear >= CurrentYear || war.EndYear == -1)))
            {
                if (war.Attacker.Parent != null)
                {
                    if (!DisplayObjects.Contains(war.Attacker.Parent)) WarEntities.Add(war.Attacker.Parent);
                }
                else if (!DisplayObjects.Contains(war.Attacker)) WarEntities.Add(war.Attacker);

                if (war.Defender.Parent != null)
                {
                    if (!DisplayObjects.Contains(war.Defender.Parent)) WarEntities.Add(war.Defender.Parent);
                }
                else if (!DisplayObjects.Contains(war.Defender)) WarEntities.Add(war.Defender);
            }
            if (FocusObject != null && (FocusObject.GetType() == typeof(Battle) || FocusObject.GetType() == typeof(SiteConquered)))
            {
                Battle battle;
                if (FocusObject.GetType() == typeof(Battle)) battle = (FocusObject as Battle);
                else battle = ((SiteConquered)FocusObject).Battle;
                if (battle != null)
                {
                    if (battle.Attacker.Parent != null)
                    {
                        if (!DisplayObjects.Contains(battle.Attacker.Parent)) WarEntities.Add(battle.Attacker.Parent);
                    }
                    else if (!DisplayObjects.Contains(battle.Attacker)) WarEntities.Add(battle.Attacker);

                    if (battle.Defender.Parent != null)
                    {
                        if (!DisplayObjects.Contains(battle.Defender.Parent)) WarEntities.Add(battle.Defender.Parent);
                    }
                    else if (!DisplayObjects.Contains(battle.Defender)) WarEntities.Add(battle.Defender);
                }
            }
            DisplayObjects.AddRange(WarEntities);
            GenerateCivPaths();
        }

        public void MakeOverlay(string overlay)
        {
            List<Location> coordinatesList = new List<Location>();
            List<int> occurences = new List<int>();
            OptionsMenu.Options.Single(option => option.Text == "Overlays").SubMenu.Options.ForEach(option => option.Toggled = false);
            OptionsMenu.Options.Single(option => option.Text == "Overlays").SubMenu.Options.Single(option => option.Text == overlay).Toggled = true;
            switch (overlay)
            {
                case "Battles":
                    World.EventCollections.OfType<Battle>().Select(battle => battle.Coordinates).ToList().ForEach(coordinates => coordinatesList.Add(coordinates));
                    break;
                case "Battles (Notable)":
                    World.EventCollections.OfType<Battle>().Where(battle => battle.Notable).Select(battle => battle.Coordinates).ToList().ForEach(coordinates => coordinatesList.Add(coordinates));
                    break;
                case "Battle Deaths":
                    foreach (Location coordinates in World.EventCollections.OfType<Battle>().GroupBy(battle => battle.Coordinates).Select(battle => battle.Key).ToList())
                    {
                        coordinatesList.Add(coordinates);
                        occurences.Add(World.EventCollections.OfType<Battle>().Where(battle => battle.Coordinates == coordinates).Sum(battle => battle.AttackerDeathCount + battle.DefenderDeathCount));
                    }
                    break;
                case "Site Population...":
                    dlgPopulation selectPopulations = new dlgPopulation(World);
                    selectPopulations.ShowDialog();
                    if (selectPopulations.SelectedPopulations.Count == 0) return;
                    foreach (Location coordinates in World.Sites.GroupBy(site => site.Coordinates).Select(site => site.Key).ToList())
                    {
                        coordinatesList.Add(coordinates);
                        occurences.Add(World.Sites.Where(site => site.Coordinates == coordinates).Sum(site => site.Populations.Where(population => selectPopulations.SelectedPopulations.Contains(population.Race)).Sum(population => population.Count)));
                    }
                    break;
                case "Site Events":
                    foreach (Location coordinates in World.Sites.GroupBy(site => site.Coordinates).Select(site => site.Key).ToList())
                    {
                        coordinatesList.Add(coordinates);
                        occurences.Add(World.Sites.Where(site => site.Coordinates == coordinates).Sum(site => site.Events.Count()));
                    }
                    break;
                case "Site Events (Filtered)":
                    foreach (Location coordinates in World.Sites.GroupBy(site => site.Coordinates).Select(site => site.Key).ToList())
                    {
                        coordinatesList.Add(coordinates);
                        occurences.Add(World.Sites.Where(site => site.Coordinates == coordinates).Sum(site => site.Events.Count(dEvent => !Legends.Site.Filters.Contains(dEvent.Type))));
                    }
                    break;
                case "Beast Attacks":
                    World.EventCollections.OfType<BeastAttack>().Select(attack => attack.Coordinates).ToList().ForEach(coordinates => coordinatesList.Add(coordinates));
                    break;
            }

            for (int i = 0; i < coordinatesList.Count; i++)
            {
                coordinatesList[i] = new Location(coordinatesList[i].X * TileSize + TileSize / 2, coordinatesList[i].Y * TileSize + TileSize / 2);
            }

            if (Overlay != null) Overlay.Dispose();
            if (occurences.Count > 0)
                Overlay = HeatMapMaker.Create(Map.Width, Map.Height, coordinatesList, occurences);
            else
                Overlay = HeatMapMaker.Create(Map.Width, Map.Height, coordinatesList);

            OverlayToggled = false;
            ToggleOverlay();
            GC.Collect();
        }

        public void ChangeMap()
        {
            OpenFileDialog openMap = new OpenFileDialog();
            openMap.ShowDialog();
            string fileName;
            bool deleteFile = false;
            if (openMap.FileName != "" && !openMap.FileName.EndsWith(".bmp") && !openMap.FileName.EndsWith(".png") && !openMap.FileName.EndsWith(".jpeg") && !openMap.FileName.EndsWith(".jpg"))
            {
                deleteFile = true;
                using (SevenZipExtractor extractor = new SevenZipExtractor(openMap.FileName))
                {
                    if (extractor.ArchiveFileNames.Count(file => file.EndsWith(".bmp") || file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg")) == 0)
                    {
                        MessageBox.Show("No Image Files Found");
                        return;
                    }
                    if (extractor.ArchiveFileNames.Count(file => file.EndsWith(".bmp") || file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg")) == 1)
                        fileName = extractor.ArchiveFileNames.Single(file => file.EndsWith(".bmp") || file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg"));
                    else
                    {
                        dlgFileSelect fileSelect = new dlgFileSelect(extractor.ArchiveFileNames.Where(file => file.EndsWith(".bmp") || file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg")).ToList());
                        fileSelect.ShowDialog();
                        if (fileSelect.SelectedFile == "") return;
                        if (File.Exists(fileSelect.SelectedFile)) { MessageBox.Show(fileSelect.SelectedFile + " already exists."); return; }
                        fileName = fileSelect.SelectedFile;
                    }
                    extractor.ExtractFiles(Directory.GetCurrentDirectory(), fileName);
                }
            }
            else fileName = openMap.FileName;

            if (fileName != "")
            {
                using (FileStream mapStream = new FileStream(fileName, FileMode.Open))
                    AlternateMap = new Bitmap(mapStream);
                if (AlternateMap.Size != Map.Size)
                {
                    Bitmap resized = new Bitmap(Map.Width, Map.Height);
                    using (Graphics resize = Graphics.FromImage(resized))
                    {
                        resize.SmoothingMode = SmoothingMode.AntiAlias;
                        resize.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        resize.DrawImage(AlternateMap, new Rectangle(0, 0, Map.Width, Map.Height));
                        AlternateMap.Dispose();
                        AlternateMap = resized;
                    }
                }
                MapUtil.AlternateMap = AlternateMap;
                if (MapUtil.AltMapAlpha == 0)
                {
                    MapUtil.AltMapAlpha = 1;
                }
                AlternateMapToggled = false;
                ToggleAlternateMap();
            }

            if (deleteFile) File.Delete(fileName);
        }

        private void ChangeAltMapTransparency(object sender, EventArgs e)
        {
            AltMapAlpha = AltMapTransparency.Value / 100.0f;
            MapUtil.AltMapAlpha = AltMapAlpha;
            Invalidate();
        }

        private PointF SiteToScreen(Location siteCoordinates)
        {
            PointF screenCoordinates = new PointF();
            screenCoordinates.X = (float)((siteCoordinates.X * TileSize - Source.X + TileSize / 2) * PixelWidth);
            screenCoordinates.Y = (float)((siteCoordinates.Y * TileSize - Source.Y + TileSize / 2) * PixelHeight);
            return screenCoordinates;
        }

        private void Pan(int xChange, int yChange)
        {
            Source.Offset(xChange, yChange);
            Center.Offset(xChange, yChange);
            HoverMenu.Open = false;
            if (xChange > 2 || xChange < -2 || yChange > 2 || yChange < -2) OptionsMenu.SelectedOption = null;
            Invalidate();
        }

        public void ZoomIn()
        {
            if (ZoomCurrent > ZoomMin)
                ZoomCurrent -= ZoomCurrent * ZoomChange;
            UpdateScales();
            Invalidate();
        }

        public void ZoomOut()
        {
            if (ZoomCurrent < ZoomMax)
                ZoomCurrent += ZoomCurrent * ZoomChange;
            UpdateScales();
            Invalidate();
        }

        private void ZoomToBounds()
        {
            ZoomCurrent = 1.0;
            while (ZoomCurrent > 0.85) ZoomCurrent -= ZoomCurrent * ZoomChange; //Zoom in to set zoom scale
            ZoomCurrent += ZoomCurrent * ZoomChange;
            UpdateScales();
            while (true) //Zoom out till bounds are within the map source
            {
                if (Source.X <= ZoomBounds.X && Source.Right >= ZoomBounds.Width && Source.Y <= ZoomBounds.Y && Source.Bottom >= ZoomBounds.Height)
                    break;
                ZoomCurrent += ZoomCurrent * ZoomChange;
                if (ZoomCurrent > ZoomMax) break;
                UpdateScales();
            }
            ZoomToBoundsOnFirstPaint = false;
        }

        private void UpdateScales()
        {
            Source.Location = new Point(Center.X - Convert.ToInt32((Width / 2) * ZoomCurrent), Center.Y - Convert.ToInt32((Height / 2) * ZoomCurrent));
            Source.Size = new Size(Convert.ToInt32(Width * ZoomCurrent), Convert.ToInt32(Height * ZoomCurrent));
            PixelWidth = Convert.ToDouble(Width) / Source.Width;
            PixelHeight = Convert.ToDouble(Height) / Source.Height;
            HoverMenu.Open = false;
            ControlMenu.MenuBox.Location = new Point(0, Height - Minimap.Height - ControlMenu.MenuBox.Height);
            YearMenu.MenuBox.Location = new Point(Minimap.Width, Height - YearMenu.MenuBox.Height);
            AltMapTransparency.Location = new Point(MiniMapAreaSideLength + YearMenu.MenuBox.Width, Height - AltMapTransparency.Height);
        }

        private Point WindowToWorldPoint(Point window)
        {
            int x = Source.X + Convert.ToInt32(window.X * ZoomCurrent);
            int y = Source.Y + Convert.ToInt32(window.Y * ZoomCurrent);
            return new Point(x, y);
        }

        public Point WindowToTilePoint(Point window)
        {
            Point tilePoint = WindowToWorldPoint(window);
            tilePoint.X = tilePoint.X / TileSize;
            tilePoint.Y = tilePoint.Y / TileSize;
            return tilePoint;
        }

        public Location WindowToWorldTile(Point window)
        {
            Point location = WindowToTilePoint(window);
            return new Location(location.X, location.Y);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateScales();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) MouseClickLocation = e.Location;
            if (e.Button == MouseButtons.Left) MousePanStart = e.Location;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Location.X > MouseClickLocation.X - 5 && e.Location.X < MouseClickLocation.X + 5
                && e.Location.Y > MouseClickLocation.Y - 5 && e.Location.Y < MouseClickLocation.Y + 5)//	e.Location == MouseClickLocation)
            {
                if (ControlMenu.SelectedOption != null)
                    ControlMenu.Click(e.X, e.Y);
                else if (YearMenu.SelectedOption != null)
                    YearMenu.Click(e.X, e.Y);
                else if (OptionsMenu.SelectedOption != null)
                    OptionsMenu.Click(e.X, e.Y);
                else if (HoverMenu.Options.Count == 1 && HoverMenu.Options.Count(option => option.SubMenu.Options.Count(option2 => option2.SubMenu != null) > 0) == 0)
                    HoverMenu.Options.First().Click();
                else if (!HoverMenu.Open && HoverMenu.Options.Count > 0) HoverMenu.Open = true;
                else if (HoverMenu.Open)
                {
                    HoverMenu.Click(e.X, e.Y);
                    HoverMenu.Open = false;
                }
                else HoverMenu.Open = false;

                OptionsMenu.SelectedOption = null;
            }

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!ControlMenu.HighlightOption(e.X, e.Y) && !YearMenu.HighlightOption(e.X, e.Y) && !OptionsMenu.HighlightOption(e.X, e.Y) && !HoverMenu.Open)
            {
                List<Object> addOptions = new List<Object>();
                Location tile = WindowToWorldTile(e.Location);
                foreach (Entity civ in DisplayObjects.OfType<Entity>())
                    foreach (OwnerPeriod sitePeriod in civ.SiteHistory.Where(site => (((site.StartYear == CurrentYear && site.StartCause != "took over") || site.StartYear < CurrentYear)
                                                                                      && (((site.EndYear >= CurrentYear) || site.EndYear == -1))) && site.Site.Coordinates == tile))
                    {
                        addOptions.Add(civ);
                        addOptions.Add(sitePeriod.Site);
                    }

                foreach (Site site in DisplayObjects.OfType<Site>().Where(site => site.Coordinates == tile && !addOptions.Contains(site)))
                    addOptions.Add(site);

                if (FocusObject != null && FocusObject.GetType() == typeof(Battle) && ((Battle) FocusObject).Coordinates == tile) addOptions.Add(FocusObject as Battle);
                foreach (Battle battle in DisplayObjects.OfType<War>().SelectMany(war => war.Collections).OfType<Battle>().Where(battle => battle != FocusObject && battle.StartYear == CurrentYear && battle.Coordinates == tile))
                    addOptions.Add(battle);

                if (BattlesToggled || Battles.Count > 0)
                {
                    if (BattleLocations.Count(battle => battle == tile) > 0)
                    {
                        //List<Battle> battles = DisplayObjects.OfType<Battle>().Where(battle => battle.Coordinates == tile).ToList();
                        List<Battle> battles = Battles.Where(battle => battle.Coordinates == tile).ToList();
                        if (battles.Count > 0) addOptions.Add(battles);
                    }
                }

                HoverMenu.AddOptions(addOptions);
                if (HoverMenu.Options.Count > 0)
                {
                    HoverMenu.MenuBox.Location = WindowToTilePoint(e.Location);
                    HoverMenu.MenuBox.X = Convert.ToInt32(((HoverMenu.MenuBox.X + 1) * TileSize - Source.X) * PixelWidth);
                    HoverMenu.MenuBox.Y = Convert.ToInt32(((HoverMenu.MenuBox.Y + 1) * TileSize - Source.Y) * PixelHeight);
                    Invalidate();
                }
            }
            else
                HoverMenu.HighlightOption(e.X, e.Y);

            if (OptionsMenu.SelectedOption != null || ControlMenu.SelectedOption != null || YearMenu.SelectedOption != null) HoverMenu.Options.Clear();


            if (e.Button == MouseButtons.Left)
            {
                Point worldStart = WindowToWorldPoint(new Point(MousePanStart.X, MousePanStart.Y));
                Point worldEnd = WindowToWorldPoint(new Point(e.X, e.Y));
                int xChange = worldStart.X - worldEnd.X;
                int yChange = worldStart.Y - worldEnd.Y;

                Pan(xChange, yChange);
                MousePanStart = e.Location;
            }

            MouseLocation = e.Location;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            Focus();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {

            if (e.Delta > 0)
            {
                if (ModifierKeys == (Keys.Control | Keys.Shift)) ChangeYear(100);
                else if (ModifierKeys == Keys.Control) ChangeYear(1);
                else if (ModifierKeys == Keys.Shift) ChangeYear(10);
                else ZoomIn();
            }
            else if (e.Delta < 0)
            {
                if (ModifierKeys == (Keys.Control | Keys.Shift)) ChangeYear(-100);
                else if (ModifierKeys == Keys.Control) ChangeYear(-1);
                else if (ModifierKeys == Keys.Shift) ChangeYear(-10);
                else ZoomOut();
            }
        }

        public static List<Bitmap> CreateBitmaps(World world, DwarfObject dwarfObject)
        {
            int maxSize = world.PageMiniMap.Height > world.PageMiniMap.Width ? world.PageMiniMap.Height : world.PageMiniMap.Width;
            MapPanel createBitmaps = new MapPanel(world.Map, world, null, dwarfObject);
            createBitmaps.Height = maxSize;
            createBitmaps.Width = maxSize;
            if (createBitmaps.ZoomToBoundsOnFirstPaint) createBitmaps.ZoomToBounds();

            createBitmaps.MiniMapAreaSideLength = maxSize;
            //createBitmaps.SetupMinimap();
            createBitmaps.Minimap = world.PageMiniMap;

            Bitmap miniMap = new Bitmap(maxSize, maxSize);

            using (Graphics miniMapGraphics = Graphics.FromImage(miniMap))
                createBitmaps.DrawMiniMap(miniMapGraphics);

            Bitmap map = new Bitmap(maxSize, maxSize);

            using (Graphics mapGraphics = Graphics.FromImage(map))
            {
                //mapGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                mapGraphics.PixelOffsetMode = PixelOffsetMode.Half;
                createBitmaps.DrawMap(mapGraphics);
                createBitmaps.DrawDisplayObjects(mapGraphics);
                createBitmaps.Dispose();
                return new List<Bitmap> { map, miniMap };
            }
        }

        private Site GetClosestSite(Entity civ, Location coordinates)
        {
            double closestDistance = double.MaxValue;
            Site closestSite = null;
            foreach (OwnerPeriod period in civ.SiteHistory.Where(site => ((site.StartYear == CurrentYear && site.StartCause != "took over") || site.StartYear < CurrentYear)
                                                                         && (((site.EndYear >= CurrentYear) || site.EndYear == -1))))
            {
                int rise = (period.Site.Coordinates.Y - coordinates.Y);
                int run = (period.Site.Coordinates.X - coordinates.X);
                double distance = Math.Sqrt(Math.Pow(rise, 2) + Math.Pow(run, 2));
                if (distance < closestDistance)
                {
                    closestSite = period.Site;
                    closestDistance = distance;
                }
            }
            return closestSite;
        }
    }
}