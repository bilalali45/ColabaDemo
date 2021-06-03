import React from "react";

interface IconRadioBoxProps {
    className?: string;
    title?: string;
    handlerClick?: (id: number) => void;
    Icon?: any;
    id?: any;
    name?: string;
    value?: string;
    checked?: boolean;
    onChange?: any;
    testId?: string;
    ref?: any,
}

const IconRadioBox: React.FC<IconRadioBoxProps> = ({
    className = "",
    title = "",
    Icon,
    id,
    name,
    checked,
    value,
    handlerClick,
    ref,
    testId,
    ...rest
}) => {

    //const [getChecked, setChecked] = useState(checked);

    return (
        <label
            data-testid="list-item"
            id={`${id}`}
            className={`IconRadioBox ${checked && 'checked'}  ${className}`}
            onClick={() => { handlerClick && handlerClick(id); }}
        >
            <div className="c-wrap">
                <div className="c-h-icon">
                    {/* {checked} */}

                    <input
                        ref={ref}
                        type="radio"
                        onChange={() => { }}
                        id={"inputradio-" + id}
                        name={name}
                        checked={checked}
                        value={value}
                        {...rest}
                    />
                    <span className={`Radiobox-type`}></span>
                </div>
                <div className="i-wrap">
                    {/* <img src={Icon} /> */}
                    {Icon}
                </div>
                <div className="c-title">{title}</div>
            </div>
        </label>
    );
};

export default IconRadioBox;
