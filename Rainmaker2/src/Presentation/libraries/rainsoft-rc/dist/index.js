function _interopDefault (ex) { return (ex && (typeof ex === 'object') && 'default' in ex) ? ex['default'] : ex; }

var React = _interopDefault(require('react'));
var Dropdown = _interopDefault(require('react-bootstrap/Dropdown'));

var HeaderMenu = function HeaderMenu(_ref) {
  var displayName = _ref.displayName,
      options = _ref.options;

  var getShortName = function getShortName(name) {
    var splitData = name.split(" ");
    var shortName = splitData[0].charAt(0).toUpperCase() + splitData[1].charAt(0).toUpperCase();
    return shortName;
  };

  return React.createElement(Dropdown, {
    className: "userdropdown"
  }, React.createElement(Dropdown.Toggle, {
    id: "dropdownMenuButton",
    className: "hd-shorname",
    as: "a"
  }, React.createElement("span", null, getShortName(displayName))), React.createElement(Dropdown.Menu, null, options.map(function (item) {
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
    options: options,
    displayName: displayName
  })))))));
};

var RainsoftRcFooter = function RainsoftRcFooter(_ref) {
  var title = _ref.title,
      streetName = _ref.streetName,
      address = _ref.address,
      phoneOne = _ref.phoneOne,
      phoneTwo = _ref.phoneTwo,
      contentOne = _ref.contentOne,
      contentTwo = _ref.contentTwo,
      nmlLogoSrc = _ref.nmlLogoSrc,
      nmlUrl = _ref.nmlUrl;
  return React.createElement("section", null, React.createElement("footer", {
    className: "mainfooter"
  }, React.createElement("div", {
    className: "container"
  }, React.createElement("div", {
    className: "row"
  }, React.createElement("div", {
    className: "col-xl-4 col-lg-4 col-md-4 col-sm-12 col-12"
  }, React.createElement("h2", null, title), React.createElement("address", null, React.createElement("i", {
    className: "fas fa-map-marker-alt"
  }), React.createElement("span", null, streetName, React.createElement("br", null), address)), React.createElement("div", {
    className: "footer-phone"
  }, React.createElement("i", {
    className: "fas fa-phone"
  }), React.createElement("span", null, React.createElement("span", {
    className: "telLinkerInserted"
  }, React.createElement("a", {
    className: "telLinkerInserted",
    href: "tel:" + phoneOne,
    title: "Call: (888) 971-1425"
  }, phoneOne)), React.createElement("br", null), React.createElement("span", {
    className: "telLinkerInserted"
  }, React.createElement("a", {
    className: "telLinkerInserted",
    href: "tel:" + phoneTwo,
    title: "Call: (214) 245-3929"
  }, phoneTwo))))), React.createElement("div", {
    className: "col-xl-8 col-lg-8 col-md-8 col-sm-12 col-12"
  }, React.createElement("div", {
    className: "copyright-text"
  }, React.createElement("p", null, contentOne), React.createElement("p", null, contentTwo)), React.createElement("div", {
    className: "nmls text-right"
  }, React.createElement("a", {
    href: nmlUrl,
    target: "_blank",
    rel: "noopener noreferrer"
  }, React.createElement("img", {
    src: nmlLogoSrc,
    alt: "Illinois Residential Mortgage Licensee NMLS License #277676"
  }))))))), React.createElement("div", {
    className: "bg-shape d-none d-lg-block"
  }));
};

exports.RainsoftRcFooter = RainsoftRcFooter;
exports.RainsoftRcHeader = RainsoftRcHeader;
//# sourceMappingURL=index.js.map
