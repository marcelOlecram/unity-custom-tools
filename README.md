# Unity custom transfor inspector

A unity custom inespector adding functionality to Transform component.
----
## About
The original idea came from this [tutorial](http://naplandgames.com/blog/2016/08/27/unity-3d-tutorial-custom-transform-inspector/).
The purpose of this repository is to expand functionality.

The project uses a [MIT License](https://github.com/marcelOlecram/unity-custom-transform/blob/master/LICENSE). Use it as you want it

The basic Transform inspector in Unity only provides Position, Rotation and Scale information. This custom transform inspector, expand from this, adding:

- Real world position of the GameObject (instead of the relative position to theri parent)
- A quaternion foldout (which is main idea from the tutorial linked above).
- A set of tools to place a gameobject such as: 
    - Reset buttons to set Position to zero, Rotation to zero, and Scale to one, and one in all button
    - Random position (between a range designated from two Vector3), with an Axis constraint
    - Random rotation (calculated in EulerAngles from 0 to 360 degress), with Axis contraint
    - Random scale (calculated between two floats, this random scale is applied to all xis). Keeping uniformity


## todo repository in general
- show images maybe gifs explaining functions
- subdivide in more tools
- tool to add Joystick to Input Manager

