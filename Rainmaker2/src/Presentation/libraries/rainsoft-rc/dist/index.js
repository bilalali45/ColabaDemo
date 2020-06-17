function _interopDefault (ex) { return (ex && (typeof ex === 'object') && 'default' in ex) ? ex['default'] : ex; }

var React = _interopDefault(require('react'));
var Dropdown = _interopDefault(require('react-bootstrap/Dropdown'));

var HeaderMenu = function HeaderMenu(_ref) {
  var options = _ref.options;
  return React.createElement(Dropdown, {
    className: "userdropdown"
  }, React.createElement(Dropdown.Toggle, {
    id: "dropdownMenuButton",
    className: "hd-shorname",
    as: "a"
  }), React.createElement(Dropdown.Menu, null, options.map(function (item) {
    return React.createElement(Dropdown.Item, {
      key: item.name,
      onClick: function onClick(e) {
        return item.callback(e);
      }
    }, item.name);
  })));
};

var RainsoftRcHeader = function RainsoftRcHeader(_ref) {
  var logoSrc = _ref.logoSrc,
      displayName = _ref.displayName,
      displayNameOnClick = _ref.displayNameOnClick,
      options = _ref.options;
  return React.createElement("header", {
    className: "header-main"
  }, React.createElement("div", {
    className: "container-fluid"
  }, React.createElement("div", {
    className: "row"
  }, React.createElement("div", {
    className: "col-12"
  }, React.createElement("nav", {
    className: "navbar navbar-default"
  }, React.createElement("div", {
    className: "navbar-header h-logo"
  }, React.createElement("a", {
    className: "logo-link",
    href: "/"
  }, React.createElement("img", {
    alt: "Texas Trust Home Loans - Mortgage Lender in Texas ",
    src: logoSrc,
    className: "d-none d-sm-block"
  }))), React.createElement("div", {
    className: "s-account pull-right"
  }, React.createElement("a", {
    className: "d-name d-none d-sm-block",
    onClick: function onClick(e) {
      return displayNameOnClick(e);
    }
  }, "Hello,", displayName), React.createElement(HeaderMenu, {
    options: options
  })))))));
};

var RainsoftRcFooter = function RainsoftRcFooter(_ref) {
  var content = _ref.content;
  return React.createElement("section", null, React.createElement("footer", {
    className: "mainfooter"
  }, React.createElement("div", {
    className: "container"
  }, React.createElement("div", {
    className: "row"
  }, React.createElement("div", {
    className: "col-12 text-center"
  }, content)))));
};

exports.RainsoftRcFooter = RainsoftRcFooter;
exports.RainsoftRcHeader = RainsoftRcHeader;
//# sourceMappingURL=index.js.map
