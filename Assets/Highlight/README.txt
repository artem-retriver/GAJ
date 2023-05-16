Thanks for download asset. You can write in alexsampublic@outlook.com for questions.

## WHAT'S IN THE ASSET ? 
---------------------------------------------------------------------
There are 4 materials in the asset, materials are in RobotKyle / Materials. There is also a demo scene in the Demo folder.
The asset contains a material with a shader that will display elements that are behind walls.

## HOW CONFIGURATE SHADERS?
---------------------------------------------------------------------
-------OutlineHighlightMaterial 
All materials have MainTex - the main texture.
Simple outline effect.
Color - the color of the outer line.
Outline - line width parameter.
-------SmoothOutlineHighlightMaterial
Smooth outline effect.
Highlight Color - the color of the outer line.
HighlightStrength - strength highlight, the larger the parameter, the stronger the glow.
-------HighlightMaterial
Highlight effect using an additional texture.
Additional Texture - the additional texture will be superimposed on top of the main one.
Highlight Concentration - the larger the parameter, the more the additional texture covers the model.
Additional Color - color additional texture, highlight color.
-------AnimationHighlightMaterial
Highlight effect using additional texture and smooth concentration over time.
Highlight effect using an additional texture.
Additional Texture - the additional texture will be superimposed on top of the main one.
Additional Color - color additional texture, highlight color.
StartHighlight - start highlight concentration value.
EndHighlight - end highlight concentration value.
Speed - speed animation, concentration changes  from StartHighlight to EndHighlight
PlayOnce - when the mode is on, the animation is played once, from the start to the end position, otherwise it is played in a loop
-----------------------------------------------------------------------
## HOW CONFIGURATE WALL SHADERS?
---------------------------------------------------------------------
MainTex - the texture displayed on the element when not behind a wall.
NewTex - the texture displayed on the element when it is behind a wall.
You can also adjust the colors, main (MultiplyColor) and additional (AdditiveColor)
Outline Thickness  - you can add an outline effect. this is the effect thickness parameter
Outline Color - outline-effect color
