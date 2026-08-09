# DiagramLink.Geometry, .Style und .Path – Feldformate

`DiagramLink.Geometry` und `DiagramLink.Style` sind rohe EA-COM-Strings im Format
`Key1=Wert1;Key2=Wert2;...`, `DiagramLink.Path` ist eine geordnete Liste von Koordinatenpaaren
(`X:Y;X:Y;...`). Sparx dokumentiert offiziell nur, dass diese Properties existieren, nicht ihren
internen Aufbau. Die folgenden Tabellen fassen zusammen, was sich dazu im Web recherchieren ließ
(Stand: 2026-08-07) – belegte Aussagen sind mit Quelle gekennzeichnet, alles andere ist eine
plausible, aber unbestätigte Vermutung anhand der Namensmuster und Beispielwerte.

Beispielstring (Geometry) aus einem realen `DiagramLink`:

```
SCTR=1;SCME=1;SX=0;SY=0;EX=0;EY=0;EDGE=2;SCTR.LEFT=469;SCTR.TOP=-824;SCTR.RIGHT=552;SCTR.BOTTOM=-789;$LLB=;LLT=CX=86:CY=14:OX=31:OY=20:HDN=0:BLD=0:ITA=0:UND=0:CLR=-1:ALN=1:DIR=0:ROT=0;LMT=;LMB=;LRT=;LRB=;IRHS=;ILHS=;
```

## Geometry – Top-Level-Keys

| Key | Bedeutung | Status |
|---|---|---|
| `SX`, `SY` | Offset relativ zur Mitte des **Start**-Objekts | Belegt (Capri-Soft) |
| `EX`, `EY` | Offset relativ zur Mitte des **End**-Objekts | Belegt (Capri-Soft) |
| `EDGE` | Austrittsseite am Start-Objekt: 1=Oben, 2=Rechts, 3=Unten, 4=Links | Belegt (Capri-Soft) |
| `SCTR` | Vermutlich Flag/Modus für Segment-Routing (im Beispiel `1`) | Unbekannt/spekulativ |
| `SCME` | Unbekannt (im Beispiel `1`) | Unbekannt/spekulativ |
| `SCTR.LEFT/TOP/RIGHT/BOTTOM` | Wirken wie ein Bounding-Rect (passend zu Element-Koordinaten im Beispiel) | Unbekannt/spekulativ |
| `LLT`, `LMT`, `LMB`, `LRT`, `LRB` | Vermutlich Label-Ankerpunkte: Left/Middle/Right × Top/Bottom | Unbekannt/spekulativ |
| `$LLB` | Vermutlich weiterer Label-Anker (Left-Bottom?), `$`-Präfix ungeklärt | Unbekannt/spekulativ |
| `IRHS`, `ILHS` | Vermutlich "rechte/linke Seite"-Bezug | Unbekannt/spekulativ |

## Label-Teilstring (verschachtelter Wert von z. B. `LLT=`, `LMT=`, …)

| Sub-Key | Bedeutung | Status |
|---|---|---|
| `CX`, `CY` | Größe der Label-Textbox | Belegt (Sparx-Forum, Thread 4376) |
| `OX`, `OY` | Vermutlich Positions-Offset des Labels | Unbekannt — selbst Sparx-Forenteilnehmer konnten es nicht erklären |
| `HDN` | Vermutlich "Hidden"-Flag | Unbekannt/spekulativ |
| `BLD`, `ITA`, `UND` | Vermutlich Bold/Italic/Underline-Flags | Unbekannt/spekulativ |
| `CLR` | Vermutlich Textfarbe (`-1` = Standard) | Unbekannt/spekulativ |
| `ALN` | Vermutlich Ausrichtung (Alignment) | Unbekannt/spekulativ |
| `DIR` | Unbekannt | Unbekannt/spekulativ |
| `ROT` | Vermutlich Rotation des Labels | Unbekannt/spekulativ |

## Style – Top-Level-Keys

| Key | Bedeutung | Status |
|---|---|---|
| `Mode` | Linienführung: 1=Direct, 2=Auto-Routing, 3=Custom Line | Belegt (Capri-Soft) |
| `TREE` | Nur bei `Mode=3`: `V, H, LV, LH, OS, OR` — z. B. `OR`=orthogonal gerundet | Belegt (Capri-Soft, deckt sich mit `LinkLineStyle` im eigenen Code) |
| `EOID`, `SOID` | Vermutlich End-/Start-Objekt-ID | Unbekannt/spekulativ, aber plausibel laut Beispielwerten |
| `Color` | Linienfarbe (`-1` = Standard) | Unbekannt/spekulativ (naheliegend aus Namen) |
| `LWidth` | Linienstärke | Unbekannt/spekulativ (naheliegend aus Namen) |

## Path

| Element | Bedeutung | Status |
|---|---|---|
| `X:Y;X:Y;...` | Geordnete Liste von Zwischenpunkten (Wegpunkten) der Linie | Belegt (Capri-Soft, ohne Detailtiefe) |

## Fazit

Nur die mit "Belegt" markierten Zeilen stammen aus tatsächlich gefundenen Quellen; der Rest ist eine
plausible, aber unbestätigte Vermutung anhand der Namensmuster und Beispielwerte. Für
`DiagramLinkManipulationExtensions` (`src/MDD4All.EA.Manipulations/DiagramLinkManipulationExtensions.cs`)
ist das unkritisch, da dort jeder Key ohnehin als opaker String behandelt wird, ohne seine Bedeutung
kennen zu müssen.

## Quellen

- [Sparx Systems Enterprise Architect + SQL: Interpretation der Linkdarstellung (Capri-Soft)](https://www.capri-soft.de/blog/?p=2904)
- [DiagramLink Geometry and Style (Sparx Forum)](https://sparxsystems.com/forums/smf/index.php?topic=2301.0)
- [Geometry values for DiagramLinks (Sparx Forum)](https://sparxsystems.com/forums/smf/index.php?topic=4376.0)
