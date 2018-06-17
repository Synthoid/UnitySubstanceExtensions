---
layout: docs
title: SubstanceParameter
description: Documentation for SubstanceParameter.
group: code
---

SubstanceParameter
----------------------------

Makes selecting Substance input parameters much easier by creating a dropdown for a referenced SubstanceGraph's inputs. Parameters can be grouped to produce nested dropdown options.

![Inspector](../images/code/SubParam01.png)

### Code
SubstanceParameter is a serialized class that allows for easy selection of a SubstanceGraph's input parameters. To use it, add a public or serialized variable of type SubstanceParameter to your class.

![Declaration](../images/code/SubParam02.png)

This will allow you to select from a target SubstanceGraph's input parameters. To actually access the parameter name, use the `.parameter` variable of the SubstanceParameter.

![Parameter access](../images/code/SubParam03.png)

### Inspector
Once you have declared a SubstanceParameter variable, you will see the following field:

![Inspector (no reference)](../images/code/SubParam04.png)

Drag a SubstanceGraph into the field to select from its parameters.

![Inspector (reference)](../images/code/SubParam05.png)

You can right click on SubstanceParameter field labels to show/hide the graph being referenced. This is mostly so developers can easily see which SubstanceGraph assets are being referenced per field.

![Inspector (show asset)](../images/code/SubParam06.png)
