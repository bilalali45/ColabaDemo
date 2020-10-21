import React, { useEffect, useState, useContext } from "react";
import Carousel from "react-bootstrap/Carousel";

import Lpstep1 from "../../../../assets/images/lp-step1.svg";
import Lpstep2 from "../../../../assets/images/lp-step2.svg";
import Lpstep3 from "../../../../assets/images/lp-step3.svg";
import Lpstep4 from "../../../../assets/images/lp-step4.svg";
import Lpstep5 from "../../../../assets/images/lp-step5.svg";
import { LaonActions, statusText } from "../../../../store/actions/LoanActions";
import { LoanProgress as LoanProgressModel } from "../../../../entities/Models/LoanProgress";
import { Loader } from "../../../../shared/Components/Assets/loader";
import { Auth } from "../../../../services/auth/Auth";
import { Store } from "../../../../store/store";
import { LoanType } from "../../../../store/reducers/loanReducer";

export const LoanProgress = () => {
  const [index, setIndex] = useState(0);

  const { state, dispatch } = useContext(Store);
  const { loan } = state;
  const { loanProgress } = loan as Pick<LoanType, "loanProgress">;
  const [currentItem, setCurrentItem] = useState<LoanProgressModel>();

  const loanProgressImages = [
    {
      order: 1,
      img: Lpstep1,
    },
    {
      order: 2,
      img: Lpstep2,
    },
    {
      order: 3,
      img: Lpstep3,
    },
    {
      order: 4,
      img: Lpstep4,
    },
    {
      order: 5,
      img: Lpstep5,
    },
  ];

  const fetchLoanProgress = async () => {
    let applicationId = Auth.getLoanAppliationId();

    let loanProgress: LoanProgressModel[] = await LaonActions.getLoanProgressStatus(
      applicationId ? applicationId : "1"
    );

    if (loanProgress) {
      dispatch({ type: "FETCH_LOAN_PROGRESS", payload: loanProgress });
      let activeStep: any = loanProgress.find((l: any) => l.isCurrent);
      setCurrentItem(() => activeStep);
    }
  };

  useEffect(() => {
    if (loanProgress?.length) return;

    fetchLoanProgress();
  }, [loanProgress]);

  useEffect(() => {
    if (!!currentItem || !loanProgress) return;

    let activeStep: any = loanProgress.find((l: any) => l.isCurrent);

    setCurrentItem(() => activeStep);
    if (activeStep) {
      let a = activeStep.order - 1;
      setIndex(a);
    }
  }, [currentItem, loanProgress]);

  function handleSelect(selectedIndex: any, e: any) {
    setIndex(selectedIndex);
  }

  const renderCarousel = () => {
    return (
      <Carousel
        as="div"
        activeIndex={index}
        onSelect={handleSelect}
        touch={true}
        controls={false}
        indicators={false}
        wrap={false}
        interval={null}
        nextIcon={
          <i aria-hidden="true" className="zmdi zmdi-chevron-right"></i>
        }
        nextLabel=""
        prevIcon={<i aria-hidden="true" className="zmdi zmdi-chevron-left"></i>}
        prevLabel=""
        fade={true}>
        {renderCarouselItems()}
      </Carousel>
    );
  };

  const renderCarouselItems = () => {
    return loanProgress?.map((l: any, i: number) => {
      console.log(l.icon)
      return (
        <Carousel.Item key={l.name}>
          <div className="lp-list">
            <div className={`step-count s-icon-${l.order}`} >
              <img
                src={`data:image/svg+xml;utf8,${encodeURIComponent(l.icon)}`}
                alt={l.order}
              />
            </div>
            <div className="lp-content">
              <div className="step-label">{l.status}</div>
              <h6>{l.name}</h6>
              <p>{l.description}</p>
            </div>
          </div>
        </Carousel.Item>
      );
    });
  };

  const renderCarouselList = () => {
    var totallist = loanProgress?.length || 0;
    var id = index;
    return loanProgress?.map((l: any, i: number) => {
      let liclass = "completed-icon";
      liclass =
        l.status == statusText.CURRENT
          ? "current-icon"
          : l.status == statusText.UPCOMMING
          ? "upcoming-icon"
          : "completed-icon";
      let step = i + 1;
      var activeindex = i === id ? " active" : "";
      return (
        <li
          key={l.name}
          data-index={activeindex}
          className={liclass + activeindex}>
          <a data-testid="steps-icon" onClick={(e) => handleSelect(i, e)}>
            {i == totallist - 1 && l.status == statusText.UPCOMMING ? (
              <i className="zmdi zmdi-flag"></i>
            ) : l.status == statusText.COMPLETED ? (
              <i className="zmdi zmdi-check"></i>
            ) : l.status == statusText.CURRENT ? (
              <i className="zmdi zmdi-male-alt"></i>
              // {<i className="zmdi zmdi-check"></i>}
            ) : (
              <span>{step}</span>
            )}
          </a>
        </li>
      );
    });
  };

  const renderSpecialItem = () => {
    return loanProgress?.map((l: any, i: number) => {
      return (
        <div key={l.name}>
          <div className="lp-list">
            <div className="lp-content">
              <h6>{l.name}</h6>
              <p>{l.description}</p>
            </div>
            <div className="status-icon" >
              <img
                src={`data:image/svg+xml;utf8,${encodeURIComponent(l.icon)}`}
                alt={l.order}
              />
            </div>
          </div>

        </div>
      );
    });
  };
  const noMilestone = () => {
    return (
      <Loader containerHeight={"308px"} marginBottom={"15px"} />
    );
  };
  if (!loanProgress) {
    return noMilestone();
  }

  if (!currentItem) {
    return <Loader containerHeight={"308px"} marginBottom={"15px"} />;
  }

  const SpecialStatus = () => {
    return (
      <div className={"lp-wrap sp-status"}>
        <div className="list-wrap">{renderSpecialItem()}</div>
      </div>
    );
  };



  const NormalStatus = () => {
    return (
      <div
        className={
          index === currentItem.order - 1
            ? "lp-wrap current-step"
            : index > currentItem?.order - 1
            ? "lp-wrap upcoming-step"
            : "lp-wrap"
        }>
        <div className="list-wrap">{renderCarousel()}</div>
        {loanProgress && loanProgress[0].milestoneType !== 2 ? (
          <div className="lp-footer">
            <ul>{renderCarouselList()}</ul>
          </div>
        ) : null}
      </div>
    );
  };

  return (
    <div data-testid="loan-progress" className="LoanProgress box-wrap">
      <div className="box-wrap--header">
        <h2 className="heading-h2"> Your Loan Status </h2>
      </div>
      <div className="box-wrap--body">
        {loanProgress && loanProgress[0].milestoneType !== 2
          ? NormalStatus()
          : SpecialStatus()}
      </div>

      {/* </div> */}
    </div>
  );
};
