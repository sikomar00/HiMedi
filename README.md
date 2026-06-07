# HiMedi · 아이들을 위한 AR 명상앱 🕯️

> 초등학생도 자연스럽게 명상에 몰입할 수 있도록, **AR(증강현실) 기반 인터랙티브 명상 콘텐츠**를 담은 모바일 앱입니다.
>
> 🏆 **2023 XR 디바이스 콘텐츠 메이커톤** (정보통신산업진흥원 주최) **우수상** 수상 — 상금 700만원

---

## 🎯 프로젝트 개요

명상은 집중력·정서 안정에 도움이 되지만, 어린이가 가만히 앉아 호흡에만 집중하기는 쉽지 않습니다. **HiMedi**는 색칠·숫자 카운팅·촛불 일렁임 같은 직관적인 시각·촉각 상호작용을 AR로 풀어내, 아이들이 놀이처럼 명상에 빠져들 수 있도록 설계한 콘텐츠입니다.

전용 교구인 **AR 명상북**(Vuforia 이미지 타겟)을 카메라로 비추면 페이지 위에 3D 그래픽이 살아나고, 아이가 손으로 조작하며 호흡·집중 루틴을 따라갈 수 있습니다.

- **개발 기간**: 2023.10.01 ~ 2023.11.24 (약 8주)
- **팀 구성**: 5명 — 기획 2 / 디자인 1 / 개발 2
- **플랫폼**: Android (Unity 빌드)

---

## ✨ 핵심 기능

| 기능 | 설명 |
|---|---|
| 🎨 **AR 색칠하기** | 카메라가 명상북 페이지를 인식하면 3D 오브젝트 위에 실시간 색을 입힐 수 있습니다. |
| 🔢 **숫자 카운팅** | 호흡에 맞춰 숫자가 순차적으로 등장하며, 들숨·날숨 페이스 메이커 역할을 합니다. |
| 🕯️ **촛불 일렁임** | 마이크 입력에 반응해 촛불이 흔들리거나 꺼지는 인터랙션 — 호흡 조절을 시각적으로 유도합니다. |
| 📖 **이미지 타겟 인식** | Vuforia로 AR 명상북 각 페이지를 인식하고 페이지별 콘텐츠를 트리거합니다. |

---

## 🛠️ 기술 스택

- **엔진**: Unity (C#)
- **AR SDK**: Vuforia (QCAR)
- **셰이더**: ShaderLab, HLSL
- **협업 도구**: Notion, Figma

---

## 📂 프로젝트 구조

```
HiMedi/
├── Assets/             # 씬, 스크립트, 3D 모델, UI 리소스
├── Packages/           # Unity 패키지 의존성
├── ProjectSettings/    # Unity 프로젝트 설정
├── QCAR/               # Vuforia(AR) 설정 및 타겟 데이터
├── .vscode/
└── HiMedi.zip          # 빌드 아카이브
```

---

## 👤 담당 역할 (김영식)

기획·개발 양쪽에 참여했으며, 주로 다음을 담당했습니다.

- **앱 전체 UI 및 메인 콘텐츠 개발** — Unity 기반 화면 전환·인터랙션 흐름 구현
- **AR 명상북 이미지 타겟 인식 기능 구현** — Vuforia 연동, 페이지별 콘텐츠 호출 로직 작성
- **메인 화면 인터랙션 구현** — 사용자 입력에 따른 오브젝트 반응 처리
- **상호작용 구조 설계** — 색칠·숫자 카운팅·촛불 일렁임 등 아동 몰입을 높이는 3D 에셋 인터랙션 기획·구현

---

## 🎬 시연 & 산출물

- 📺 **초등학생 대상 시연 영상**: [YouTube](https://youtu.be/txzosnhA6E4?t=29)
- 📺 **메인 기능 시연 영상**: [YouTube](https://youtu.be/f85FlpePHr0)
- 🖼️ **스토리보드 / 플로우차트** (Figma): [보러가기](https://www.figma.com/board/mzn7MXOgMpVlZAG65hrFxl)
- 📑 **발표 PPT**: [Google Drive](https://drive.google.com/file/d/1qzR3IoFGREUmvHEatQ5tpGPkgXZYke2w/view)

---

## 🚀 빌드 & 실행

> Unity Editor에서 직접 열어보려면 아래 환경을 권장합니다.

1. **Unity Hub**에서 본 리포를 프로젝트로 추가
2. Unity Editor에서 프로젝트 열기 *(Vuforia가 포함된 버전 사용)*
3. `Assets/Scenes/` 의 메인 씬 열기
4. **Android Build Support** 모듈 설치 후 빌드 → `.apk` 생성
5. Vuforia 카메라 권한 활성화 후 실행

> 빌드된 `HiMedi.zip` 아카이브가 리포 루트에 포함되어 있습니다.

---

## 🏆 수상

**2023 XR 디바이스 콘텐츠 메이커톤** — 우수상 (정보통신산업진흥원, 2023.11)

- 공모전 자료: [발표자료 PDF](https://drive.google.com/file/d/1qzR3IoFGREUmvHEatQ5tpGPkgXZYke2w/view)

---

## 📬 Contact

**김영식**
- ✉️ dolevi4k@gmail.com
- 🔗 [LinkedIn](https://www.linkedin.com/in/sikomar/)
- 🐙 [GitHub](https://github.com/sikomar00)
