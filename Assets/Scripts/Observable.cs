﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observable {

    void AddObserver(Observer observer);
    void InformObservers();
}
