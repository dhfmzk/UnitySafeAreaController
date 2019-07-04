# UnitySafeAreaController
![unity](https://img.shields.io/badge/unity-2017.4.29f1-lightgrey.svg)
![last-commit](https://img.shields.io/github/last-commit/rlatkdgus500/UnitySafeAreaController.svg)
![issues](https://img.shields.io/github/issues/rlatkdgus500/UnitySafeAreaController.svg)
![license](https://img.shields.io/github/license/rlatkdgus500/UnitySafeAreaController.svg)
> This plugin allows you to easily check and test the Safe Area for iPhone X in Unity Editor

## KeyFeature
There are two main ways to support SafeArea on iOS.
1. control `Camera.rect`
2. control `Canvas & Sub-Canvas`

This project supports both of the above methods and is intended to be applied to various structures.

## How to Use
### Canvas based method
1. Add `SafeAreaConroller` in your root canvas
2. `control type` set `Canvas Based`
3. Choose safeArea update timing (can multi-select)
4. Add `CanvasPropertyOverrider` in your sub-canvas
5. If the canvas should be inside the `SafeArea`, check `is Safe Canvas`

### Camera based method
1. Add `SafeAreaConroller` in your root canvas
2. `control type` set `Camera Based`
3. Choose safeArea update timing (can multi-select)
4. Add `CameraPropertyOverrider` in your camera obejct
5. If camera screen should be inside the `SafeArea`, check `is Safe Area Camrea`

## To Do
- [X] Support Android `cut off` and soft-key area
- [ ] Support all rotation
- [ ] Add Tester in unity editor
- [ ] Show Notch line
- [ ] Add new logo

## License

<img align="right" src="http://opensource.org/trademarks/opensource/OSI-Approved-License-100x137.png">

The class is licensed under the [MIT License](http://opensource.org/licenses/MIT):

Copyright &copy; 2017 [Sang Hyeon Kim](http://www.github.com/rlatkdgus500).

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
