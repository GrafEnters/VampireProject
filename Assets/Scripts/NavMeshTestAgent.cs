using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTestAgent : MonoBehaviour {
    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    private Transform _target;

    private void Start() {
        SetRndDest();
    }

    private void SetRndDest() {
        _agent.SetDestination(new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10)));
    }

    private void Update() {
        _agent.SetDestination(_target.position);
    }

    public class Solution {
        public string AddBinary(string a, string b) {
            int maxLength = Math.Max(a.Length, b.Length);
            int saved = 0;
            List<char> res = new List<char>(maxLength + 1);

            for (int i = 1; i < maxLength; i++) {
                int sum = saved;
                if (a.Length >= i && a[^i] == '1') {
                    sum++;
                }

                if (b.Length >= i && b[^i] == '1') {
                    sum++;
                }

                switch (sum) {
                    case 0:
                        res.Add('0');
                        break;
                    case 1:
                        res.Add('1');
                        saved = 0;
                        break;
                    case 2:
                        res.Add('0');
                        saved = 1;
                        break;
                    case 3:
                        res.Add('1');
                        break;
                }
            }

            if (saved == 1) {
                res.Add('1');
            }

            res.Reverse();

            return new string(res.ToArray());
        }
    }
}