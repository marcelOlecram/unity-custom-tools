# Transform Editor Additional Tools

A Custom inspector for Unity's Transform component designed to expand functionality
----
## About
Main idea based on this [tutorial](http://naplandgames.com/blog/2016/08/27/unity-3d-tutorial-custom-transform-inspector/).

The project uses a [MIT License](https://github.com/marcelOlecram/unity-custom-transform/blob/master/LICENSE). Use it as you want it

The basic Transform inspector in Unity only provides Position, Rotation and Scale information. This custom inspector, expand from this, adding:

- Real world position of the GameObject (instead of the relative position to theri parent)
- A quaternion foldout (which is main idea from the tutorial linked above).
- A set of tools to place a gameobject such as: 
    - Reset buttons to set Position to zero, Rotation to zero, and Scale to one, and one in all button
    - Random position (between a range designated from two Vector3), with an Axis constraint
    - Random rotation (calculated in EulerAngles from 0 to 360 degress), with Axis contraint
    - Random scale (calculated between two floats, this random scale is applied to all xis). Keeping uniformity

## Ideas to add in future
- make repo more presentable
- subdivide in more tools
