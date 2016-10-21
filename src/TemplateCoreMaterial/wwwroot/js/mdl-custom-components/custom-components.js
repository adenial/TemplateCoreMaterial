function Deletable(element)
{
  'use strict';
  this.element_ = element;
  this.init();
}

Deletable.prototype.init = function ()
{
  "use strict";
};

Deletable.prototype.remove = function ()
{
  "use strict";
  // this.element.remove
};

Deletable.prototype.CssClasses_ =
{
};

componentHandler.register(
{
  constructor: Deletable,
  classAsString: 'Deletable',
  cssClass: 'js-deletable'
});