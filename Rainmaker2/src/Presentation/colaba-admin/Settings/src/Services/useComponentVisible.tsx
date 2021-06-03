import { useState, useEffect, useRef } from 'react';
import ReactDOM from "react-dom";

export default function useComponentVisible(initialIsVisible:any) {
    const [isComponentVisible, setIsComponentVisible] = useState(initialIsVisible);
    const ref = useRef(null);

    const handleHideDropdown = (event: KeyboardEvent) => {
        if (event.key === "Escape") {
          setIsComponentVisible(false);
        }
      };

    const handleClickOutside = (event:any) => {
        if (ref.current) { // && !ref.current.contains(event.target)
            setIsComponentVisible(false);
        }
    };

    useEffect(() => {
        document.addEventListener('click', handleClickOutside, true);
        return () => {
            document.removeEventListener('click', handleClickOutside, true);
        };
    },[]);

    return { ref, isComponentVisible, setIsComponentVisible };
}