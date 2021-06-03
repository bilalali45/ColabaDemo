import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { LoanPurposeType } from "../../../../Entities/Models/types";
import IconRadioBox from "../../../../Shared/Components/IconRadioBox";
import PageHead from "../../../../Shared/Components/PageHead";
import { IconCooperative, IconSingleFamilyProperty, IconTownhouse, IconCondominium, IconDuplex2Unit, IconManufacturedHome, IconTriplex3Unit, IconQuadplex4Unit, IconLandProperty } from "../../../../Shared/Components/SVGs";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";
import { SectionTypeEnum } from "../../../../Utilities/Enumerations/MyPropertyEnums";
import { TenantConfigFieldNameEnum } from "../../../../Utilities/Enumerations/TenantConfigEnums";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";


type props = {
  propertyTypes: LoanPurposeType[];
  selectedPropertyType: number;
  saveHandler: Function;
  setSelectedPropertyType: Function;
};

export const SubjectPropertyTypeWeb = ({ propertyTypes, saveHandler, selectedPropertyType, setSelectedPropertyType }: props) => {

  const { handleSubmit } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "firstError",
    shouldFocusError: true,
    shouldUnregister: true,
  });
  const [propertyTypeId, setPropertyTypeId] = useState<number | undefined>(undefined);
  const [btnClick, setBtnClick] = useState<boolean>(false);

  useEffect(() => {
    setPropertyTypeId(selectedPropertyType);
  }, [selectedPropertyType])

  const onClickSaveBtn = async () => {
    if (!btnClick) {
      setBtnClick(true);
      saveHandler(propertyTypeId);
    }
  }

  const onSkip = async () => {
    NavigationHandler.moveNext();
  }

  const icons = {
    'Single Family Property': <IconSingleFamilyProperty />,
    'Townhouse': <IconTownhouse />,
    'Condominium': <IconCondominium />,
    'Cooperative': <IconCooperative />,
    'Duplex (2 Unit)': <IconDuplex2Unit />,
    'Manufactured Home': <IconManufacturedHome />,
    'Triplex (3 Unit)': <IconTriplex3Unit />,
    'Quadplex (4 Unit)': <IconQuadplex4Unit />,
    'Land': <IconLandProperty />,
  }

  const renderPropertyTypes = () => {

    return (
      <>
        {propertyTypes?.map((item: LoanPurposeType) => {
          return (
            <div data-testid="property-list" className="col-md-6">
              <IconRadioBox
                id={item.id}
                className={selectedPropertyType ? selectedPropertyType === item.id ? "active" : "" : ""}
                name="radio1"
                checked={selectedPropertyType === item.id ? true : false}
                value={item.name}
                title={item.name}
                Icon={icons[item.name]}
                handlerClick={(id) => {
                  setSelectedPropertyType(id);
                  setPropertyTypeId(id);
                }}
              />
            </div>
          );
        })}
      </>
    );
  };

  const isFieldRequired = (sectionType: SectionTypeEnum) => {
    switch (sectionType) {
      case SectionTypeEnum.CurrentResidence:
      case SectionTypeEnum.AdditionalProperty:
        return NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.PropertyTypeMyProperties, true);
      case SectionTypeEnum.SubjectProperty:
        return NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.PropertyTypeSubjectProperty, true);
      default:
        return true;
    }
  }

  return (
    <div className="compo-subject-P compo-subject-P-new fadein">
      <PageHead title="Subject Property" />

      <TooltipTitle title="Please let us know about your new home." />
      <form data-testid="subjectProperty-form">
        <div className="comp-form-panel colaba-form">
          <div className="form-group">
            <h4>My new home is a</h4>
          </div>


          <div className="row form-group">
            {renderPropertyTypes()}
          </div>

          <div className="form-footer">
            <button
              data-testid="save-btn"
              disabled={selectedPropertyType ? false : true}
              className="btn btn-primary"
              onClick={handleSubmit(onClickSaveBtn)}>
              {"Save & Continue"}
            </button>
            {
              !isFieldRequired(SectionTypeEnum.SubjectProperty) &&
              <button id="skip-btn" style={{ marginLeft: 10 }} data-testid="skip-btn" className="btn btn-primary" type="button" onClick={onSkip} >
                {'Skip'}
              </button>
            }
          </div>
        </div>
      </form>
    </div>
  );
};
